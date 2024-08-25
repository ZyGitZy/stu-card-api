using stu_card_api.Entitys;
using stu_card_entity_store.Store;

namespace stu_card_api.Services
{
    public class TemplateService
    {
        IEntityStore<TemplateEntity> entityStore;
        public TemplateService(IEntityStore<TemplateEntity> entityStore)
        {
            this.entityStore = entityStore;
        }

    }
}
