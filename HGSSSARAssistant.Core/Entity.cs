using System;
namespace HGSSSARAssistant.Core
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        public long Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity;
            return entity != null &&
                   Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
