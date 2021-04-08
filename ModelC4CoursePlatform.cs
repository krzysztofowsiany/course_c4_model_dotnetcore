using modelc4_project.Layers;
using Structurizr;
using Structurizr.Api;

namespace modelc4_project
{
    internal class ModelC4CoursePlatform
    {
        private ModelC4Config _config;
        private Workspace _workspace;

        public ModelC4CoursePlatform(ModelC4Config config)
        {
            _config = config;
            _workspace = new Workspace("Course Platform", "Sell and delivery courses");
            
            new C1(_workspace);
            new C2(_workspace);
            new C3(_workspace);
        }

        internal void Publish()
        {
            var structrizrClient = new StructurizrClient(_config.ApiKey, _config.ApiSecret);

            structrizrClient.PutWorkspace(_config.WorkspaceId, _workspace);
        }
    }
}