using System;
using Structurizr;

namespace modelc4_project.Layers.C1 {
    public class C1Builder {
        private Workspace _workspace;

        internal C1Builder(Workspace workspace) {
            _workspace = workspace;
        }

        internal C1Builder AddPerson(string name, string desc) {
            _workspace.Model.AddPerson(name, desc);
            return this;
        }

        internal C1Builder PersonUses(string personName, string softwareSystemName, string desc) {
            var person = _workspace.Model.GetPersonWithName(personName);
            var softwareSystem = _workspace.Model.GetSoftwareSystemWithName(softwareSystemName);
            person.Uses(softwareSystem, desc);

            return this;
        }

        private void Prepare() {
        
           
        }

        internal C1Builder SoftwareSystemUses(string sourceSoftwareSystemName, string destinationSoftwareSystemName, string desc)
        {
            var sourceSoftwareSystem = _workspace.Model.GetSoftwareSystemWithName(sourceSoftwareSystemName);
            var destinationSoftwareSystem = _workspace.Model.GetSoftwareSystemWithName(destinationSoftwareSystemName);
            sourceSoftwareSystem.Uses(destinationSoftwareSystem, desc);

            return this;
        }

        internal C1Builder AddStyle(ElementStyle elementStyle)
        {
           _workspace.Views.Configuration.Styles.Add(elementStyle);

           return this;
        }

        internal C1Builder SoftwareSystemDelivers(string softwareSystemName, string personName, string desc)
        {
            var softwareSystem = _workspace.Model.GetSoftwareSystemWithName(softwareSystemName);
            var person = _workspace.Model.GetPersonWithName(personName);
            softwareSystem.Delivers(person, desc);

            return this;
        }

        internal SoftwareSystem GetSoftwareSystem(string softwareSystemName)
        {
            return _workspace.Model.GetSoftwareSystemWithName(softwareSystemName);
        }

        internal C1Builder AddSoftwareSystem(string name, string desc, string tag = null) {
            var softwareSystem = _workspace.Model.AddSoftwareSystem(name, desc);

            if (!string.IsNullOrEmpty(tag)) {
                softwareSystem.AddTags(tag);
            }

            return this;
        }

        internal C1Builder BuildView() {

          

            return this;
        }
    }
}