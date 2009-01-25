using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearWarrior.Entities
{
    public class Entity : IEntity
    {
        public EntityRequest GetWorldRequest()
        {
            return new EntityRequest(ActionRequest.None);
        }

        public void ReceiveResponse(EntityResponse response)
        {
            throw new System.NotImplementedException();
        }

        public EntityRequest GetRequest()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IEntity
    {
        void ReceiveResponse(EntityResponse response);
        EntityRequest GetRequest();
    }
}
