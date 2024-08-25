##########################################################################
# This is the database migration create script for PowerShell.
##########################################################################

# DEV鐨勬暟鎹簱鏇存柊锛岃鍏堥厤缃互涓嬬幆澧冨彉閲?
# $ENV:ASPNETCORE_ENVIRONMENT = "Production"
Param(
    [Alias("a")]
    [switch]$Add,
    [Alias("r")]
    [switch]$Remove,
    [Alias("s")]
    [switch]$Squash,
    [Alias("u","update","update-db","update-database")]
    [switch]$UpdateDatabase,
    [Alias("sql","output-sql")]
    [switch]$OutputSql,
    [switch]$Down,
    [Alias("m","migration","migration-key")]
    [string]$MigrationKey,
    [Alias("from", "migration-from","migration-from-key")]
    [string]$MigrationFromKey,
    [Alias("to", "migration-to", "migration-to-key")]
    [string]$MigrationToKey,
    [Alias("d", "db","db-context")]
    [string]$DbContext,
    [ValidateSet("q", "quiet", "m", "minimal", "n", "noraml", "d", "detailed", "diag", "diagnostic")]
    [Alias("v", "verbosity", "build-verbosity")]
    [string]$BuildVerbosity = "q"
)
 
$ModuleName = ''
$Migrations = @()
$MigrationsSaveToFolder = ''
function Main {

    BuildProject
    
    $DbContext = GetDbContext
    info find dbcontext : $DbContext

    $Module = GetModuleName 
    $ModuleName = $Module.ToUpper()[0] + $Module.Substring(1)
    $MigrationsSaveToFolder = "Migrations/"

    $DbContextCount = GetDbContexts

    if($DbContextCount.Length -gt 1){
        $MigrationsSaveToFolder+=$ModuleName
    }

    info Module Name : $ModuleName
    info Migrations Save To Folder : $MigrationsSaveToFolder

    $Migrations = GetMigrations 
    info ($Migrations | Format-Table | Out-String)

    if($Remove)
    {
        RemoveMigrations
    }

    if ($Add) {
        AddMigration
    }

    if ($Squash) {
        SquashMigrations
    }
    if ($OutputSql) {
        CreateSql
    }

    if($UpdateDatabase)
    {
        UpdateDatabase
    }

}

Function info { Write-Host ($args) -foregroundcolor 'White' }
Function cmd {Write-Host (@('$ ') + $args) -foregroundcolor 'Green'}
Function invoke {Write-Host (@('invoke method ') + $args) -foregroundcolor 'Blue'}
Function error { Write-Host ($args) -foregroundcolor 'Red' }

Function BuildProject() {
    info "Build started..."
    cmd dotnet build -v $BuildVerbosity
    dotnet build -v $BuildVerbosity
    info "Build succeeded."
}

Function GetDbContexts(){
    return dotnet ef dbcontext list --no-build;
}

# 閫夊嚭涓€涓湁鏁堢殑DbContext
Function GetDbContext() {
    invoke GetDbContext
    # 鍒楀嚭鎵€鏈夌殑DbContext
    cmd dotnet ef dbcontext list --no-build
    $dbContexts = dotnet ef dbcontext list --no-build
    info $dbContexts

    # 濡傛灉鍙湁涓€涓狣bContext鐩存帴杩斿洖
    if ($dbContexts.GetType().Name -eq "String") {
        return $dbContexts
    }

    # 濡傛灉娌℃湁杈撳叆鍙傛暟杩斿洖绗竴涓狣bContext
    if ($DbContext -eq "") {
        if (!$?) {
            error dotnet ef dbcontext list
            exit 1
        }
        return $DbContexts[0];
    }

    # 濡傛灉杈撳叆涓€涓瓧绗︾湅鍋氫负閫氳繃Index鍙朌bContext
    if ($DbContext.Length -eq 1) {
        $index = [Int]::Parse($DbContext)
        if ($index -lt 1 -or $index -gt $DbContexts.Length) {
            error  The  DbContext index invalid. DbContext no must between 1 ~ $DbContexts.Length
            exit 1;
        }
        return $DbContexts[$index - 1 ];
    }

    $DbContextLower=$DbContext.ToLower().Trim()
    # 鍏跺畠鎯呭喌涓嬶紝閫夋嫨鍜孌bContext鐨勬湯灏句竴鐩寸殑DbContext
    for ($i = 0; $i -lt $DbContexts.Length; $i++ ) {
        $item = $DbContexts[$i];
        $pos = $item.LastIndexOf('.');
        if($pos -gt 0)
        {
            $item=$item.Substring($pos+1);
        }

        if (($item.ToLower() -eq $DbContextLower) -or $DbContexts[$i].ToLower() -eq $DbContextLower ) {
            return $DbContexts[$i];
        }
    }
    error DbContext "$DbContext" not found. 
    exit 1;
}

Function GetModuleName() {
    $from = $DbContext.LastIndexOf(".");
    if ($from -lt 0) {
        return $DbContext
    }

    $name = $DbContext.Substring($from + 1)
    $to = $name.LastIndexOf("DbContext");

    if ($to -lt 0) {
        return $name;
    }

    return $name.Substring(0, $to);
}

Function GetMigrations() {
    invoke GetMigrations
    cmd dotnet ef migrations list -c $DbContext --json --no-build

    # 杈撳嚭json鏍煎紡鏄瓧绗︿覆鏁扮粍锛屼粠绗簩琛屽紑濮嬫墠鏄疛son鐨勭殑寮€濮?
    $JsonArray = dotnet ef migrations list -c $DbContext --json --no-build

    # 鏌ユ壘Json鐨勫紑濮嬩綅缃?
    $JsonStart = $JsonArray.IndexOf('[')
    $MigrationsList = @()
    if ($JsonStart -ge 0) {
        # 鎶妀son鐨剆tring array 鍚堝苟鎴怱tring
        $Json = [String]::Join("", $JsonArray, $JsonStart, $JsonArray.Length - $JsonStart)

        # 鎶妀son杞垚瀵硅薄
        $MigrationsList = $Json | ConvertFrom-Json
    } 

    # 鎵撳嵃Migration鏂囦欢
    # info ($MigrationsList | Format-Table | Out-String)
    return , $MigrationsList 
}

Function GetMigrationName($MigrationIndex) {
    return $ModuleName + "V" + $MigrationIndex.ToString('D3');
}

Function AddMigration {
    invoke AddMigration
    
    #銆愭柊寤恒€戠紪璇戠▼搴忥紝淇濊瘉鏂板缓瀹炰綋鑳藉璇诲彇鍒?
    cmd dotnet build -v $BuildVerbosity
    dotnet build -v $BuildVerbosity

    $Migrations = GetMigrations
    
    info Origin Migraions:
    info ($Migrations | Format-Table | Out-String)
    $MigrationNo = $Migrations.Length + 1;
    $migrationFileName = GetMigrationName $MigrationNo;

    if (!$?) {
        exit 1
    }

    # 銆愭柊寤恒€戝垱寤烘柊鐨凪igration鏂囦欢
    cmd dotnet ef migrations add $migrationFileName -c $DbContext -o $MigrationsSaveToFolder  --no-build
    dotnet ef migrations add $migrationFileName -c $DbContext -o $MigrationsSaveToFolder  --no-build

    if (!$?) {
        exit 1
    }
}

Function SquashMigrations {
    invoke SquashMigrations

    RemoveMigrations
    if (!$?) {
        exit 1
    }
   
    AddMigration

    if (!$?) {
        exit 1
    }

    cmd dotnet build -v $BuildVerbosity
    dotnet build -v $BuildVerbosity

    $Migrations = GetMigrations 
    info Migraions After Squashed:
    info ($Migrations | Format-Table | Out-String)
    
    if (!$?) {
        exit 1
    }
}

Function FindMigration($migrationKey) {
    $migrationIndex = FindMigrationIndex $migrationKey
    if ($migrationIndex -eq -1) {
        return $null;
    }
    return $Migrations[$migrationIndex]
}

Function FindMigrationIndex($migrationKey) {
    $migrationIndex = -1
    if ([int]::TryParse($migrationKey, [ref]$migrationIndex)) {
        if ($migrationIndex -lt 1 -or $migrationIndex -gt $Migrations.Length) {
            error migration index $migrationKey out of index.
            return -1;
        }
        return $migrationIndex - 1;
    }

    $tmp = $migrationKey.Trim().ToLower()
    foreach ($item in $Migrations) {
        if ($item.name.ToLower() -eq $tmp) {
            return $item;
        }
    }
    for ($i = 0; $i -lt $Migrations.Length; $i++) {
        $item = $Migrations[$i]
        if ($item.name.ToLower() -eq $tmp) {
            return $i;
        }
    }
     
    error Migration $migrationKey not found.
    return -1;
}

Function CreateSql {
    invoke  CreateSql

    cmd dotnet build -v $BuildVerbosity
    dotnet build -v $BuildVerbosity
    if (!$?) {
        exit 1
    }

    $Migrations = GetMigrations 
    if ($Migrations.Length -eq 0) {
        error No migration files.
        return;
    }

    if (($MigrationKey -eq '') -or ($MigrationFromKey -ne '' -and $MigrationToKey -ne '')) {
        if ($MigrationFromKey -eq '' -and $MigrationToKey -eq '' ) {
            $SqlMigrationToIndex = $Migrations.Length - 1
            $SqlMigrationFromIndex = $Migrations.Length - 1
        }
        else {
            if ($MigrationFromKey -eq '0' -or $MigrationFromKey -eq '') {
                $SqlMigrationFromIndex = -1
            }
            else {
                $SqlMigrationFromIndex = FindMigrationIndex $MigrationFromKey
                if (-1 -eq $SqlMigrationFromIndex) {
                    return;
                }
            }
    
            if ($MigrationToKey -eq '') {
                $SqlMigrationToIndex = $Migrations.Length - 1
            }
            else {
                $SqlMigrationToIndex = FindMigrationIndex $MigrationToKey
                if (-1 -eq $SqlMigrationToIndex) {
                    return;
                }
            }
        }
    }
    else {
        $FindedMigrationIndex = FindMigrationIndex $MigrationKey
        if (-1 -eq $FindedMigrationIndex) {
            return;
        }
        if ($MigrationFromKey -eq '' -and $MigrationToKey -eq '' ) {
            $SqlMigrationToIndex = $FindedMigrationIndex
            $SqlMigrationFromIndex = $SqlMigrationToIndex
        }
        elseif ($MigrationFromKey -ne '') {
            $SqlMigrationToIndex = $FindedMigrationIndex
            if ($MigrationFromKey -eq '0') {
                $SqlMigrationFromIndex = -1
            }
            else {
                $SqlMigrationFromIndex = FindMigrationIndex $MigrationFromKey
                if (-1 -eq $SqlMigrationFromIndex) {
                    return;
                }
            }
        }
        elseif ($MigrationToKey -ne '') {
            $SqlMigrationFromIndex = $FindedMigrationIndex
            $SqlMigrationToIndex = FindMigrationIndex $MigrationToKey
            if (-1 -eq $SqlMigrationToIndex) {
                return;
            }
        }
    }

    if ($SqlMigrationFromIndex -lt $SqlMigrationToIndex) {
        $Down = $true
    }
    elseif ($SqlMigrationFromIndex -gt $SqlMigrationToIndex) {
        $Down = $false
    }
    $SqlSuffix = '_Up.sql';
    if ($Down) {
        $SqlSuffix = '_Down.sql'
    }

    if (($SqlMigrationToIndex - $SqlMigrationFromIndex) -eq 0) {
        $sqlFile = $MigrationsSaveToFolder + "/TempMigrations_" + $Migrations[$SqlMigrationToIndex].name + $SqlSuffix
    }
    else {
        if ( $SqlMigrationFromIndex -eq -1  ) {        
            $fromName = $Migrations[0].name
        }
        else {
            $fromName = $Migrations[$SqlMigrationFromIndex].name
        }
        $toName = $Migrations[$SqlMigrationToIndex].name
        $sqlFile = $MigrationsSaveToFolder + "/TempMigrations_" + $ModuleName + '(' + $fromName.Replace($ModuleName, "" ) + "~" + $toName.Replace($ModuleName , "") + ")" + $SqlSuffix
    }

    $sqlFileExits = Test-Path $sqlFile
    if ($sqlFileExits) {
        Remove-Item $sqlFile
        cmd dotnet build -v $BuildVerbosity
        dotnet build -v $BuildVerbosity
        if (!$?) {
            exit 1
        }
    }

    if ($Down -eq $false) {
        if ( $SqlMigrationFromIndex -eq -1 ) {        
            $sqlFromName = '0'
        }
        if ( $SqlMigrationFromIndex -eq 0 ) {        
            $sqlFromName = '0'
        }
        $sqlFromName = $Migrations[$SqlMigrationFromIndex - 1].name
        $sqlToName = $Migrations[$SqlMigrationToIndex].name
    }
    else {
        if ( $SqlMigrationToIndex -eq -1 ) {        
            $sqlToName = '0'
        }
        if ( $SqlMigrationToIndex -eq 0 ) {        
            $sqlToName = '0'
        }
        $sqlFromName = $Migrations[$SqlMigrationFromIndex].name
        $sqlToName = $Migrations[$SqlMigrationToIndex - 1].name
    }

    cmd dotnet ef migrations script $sqlFromName $sqlToName  -c $DbContext -o $sqlFile --no-build
    dotnet ef migrations script $sqlFromName $sqlToName  -c $DbContext -o $sqlFile --no-build
    
}

Function UpdateDatabase {
    # 鏂扮敓鎴怣igration鏂囦欢鍚庡繀椤诲湪缂栬瘧涓嬫洿鏂版暟鎹簱
    info Update Database...
    cmd dotnet build -v $BuildVerbosity
    dotnet build -v $BuildVerbosity

    if (!$?) {
        exit 1
    }

    $Migrations = GetMigrations
    if($Migrations.Length -eq 0)
    {
        error migration file not exists.
        exit -1;
    }

    $UpdateToMigrationName='';
    if($MigrationKey -ne '')
    {
        $Migaration=FindMigration($MigrationKey)
        if($Migaration -eq $null)
        {
            return
        }
        $UpdateToMigrationName=$Migaration.name
    }

    # 鏇存柊鏁版嵁搴?
    info dotnet ef database update $UpdateToMigrationName -c $DbContext --no-build
    dotnet ef database update $UpdateToMigrationName -c $DbContext --no-build

    if (!$?) {
        exit 1
    }
}

Function RemoveMigrations {
    invoke RemoveMigrations
    # 銆愬崌绾с€戠紪璇戠▼搴忥紝淇濊瘉鏂板缓瀹炰綋鑳藉璇诲彇鍒?
    cmd dotnet build -v $BuildVerbosity
    dotnet build -v $BuildVerbosity
    if (!$?) {
        exit 1
    }
    $Migrations = GetMigrations 
    info Origin Migraions:
    info ($Migrations | Format-Table | Out-String)
    if($Migrations.Length -eq 0)
    {
        info No migrations.
        exit 0
    }

    if($Squash)
    {
        if ($Migrations.Length -eq 1) {
            error Only one migration, please squash by manually.
            exit 1;
        }
        elseif ($Migrations.Length -le 2) {
            error  Only two migrations, please squash by manually.
            exit 1;
        }

        # Squash last two migrations by default.
        $FindedMigrationIndex = $Migrations.Length -2
    }else {
        if ($Migrations.Length -le 1) {
            error Only one migration, please remove by manually.
            exit 1;
        }
        # Remove the last migration by default.
        $FindedMigrationIndex = $Migrations.Length -1
    }


    if ($MigrationKey -ne '') {
        $FindedMigrationIndex = FindMigrationIndex $MigrationKey
        if (-1 -eq $FindedMigrationIndex) {
            return;
        }        
    }
    
    if ($FindedMigrationIndex -eq 0) {
        if($Squash)
        {
            error Squash all migrations, please by manually.
        }else{
            error Remove all migrations, please by manually.
        }
        exit 1;
    }

    $RollbacktoMigration = $Migrations[$FindedMigrationIndex - 1].name

    # 銆愬崌绾с€戝洖婊氭暟鎹埌涓婁竴鐗堟湰
    cmd dotnet ef database update $RollbacktoMigration -c $DbContext --no-build
    dotnet ef database update $RollbacktoMigration -c $DbContext --no-build

    for ($i = $Migrations.Length - 1; $i -ge $FindedMigrationIndex; $i--) {
        # 銆愬崌绾с€戠Щ闄ゆ渶鏂扮殑Migration鏂囦欢
        cmd dotnet ef migrations remove -c $DbContext --no-build
        dotnet ef migrations remove -c $DbContext --no-build

        if($i -gt $FindedMigrationIndex)
        {
            # 銆愬崌绾с€戠Щ闄ゅ畬鏈€鏂癕igration鏂囦欢蹇呴』閲嶆柊缂栬瘧锛屼笉鐒朵細鎶igration鏂囦欢宸茬粡瀛樺湪
            cmd dotnet build -v $BuildVerbosity
            dotnet build -v $BuildVerbosity
        }
    }
}

Main
