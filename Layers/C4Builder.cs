using System;
using Structurizr;

namespace modelc4_project.Layers {
    public class C4Builder {
        private Workspace _workspace;
        private SoftwareSystem _currentSoftwareSystem;

        internal C4Builder(Workspace workspace) {
            _workspace = workspace;
        }

        internal C4Builder AddContainer(string containerName, string containerDescription, string containerTechnology, string tag = null) {
            var container = _currentSoftwareSystem.AddContainer(containerName, containerDescription, containerTechnology);

            if (!string.IsNullOrEmpty(tag)) {
                container.AddTags(tag);
            }
            return this;
        }

        internal C4Builder SelectSoftwareSystem(string softwareSystemName) {
            _currentSoftwareSystem = GetSoftwareSystem(softwareSystemName);
            return this;
        }

        internal C4Builder AddPerson(string name, string desc) {
            _workspace.Model.AddPerson(name, desc);
            return this;
        }

        internal C4Builder PersonUses(string personName, string softwareSystemName, string desc) {
            var person = _workspace.Model.GetPersonWithName(personName);
            var softwareSystem = _workspace.Model.GetSoftwareSystemWithName(softwareSystemName);
            person.Uses(softwareSystem, desc);

            return this;
        }

        internal C4Builder PersonUsesContainer(string personName, string containerName, string desc) {
            var person = _workspace.Model.GetPersonWithName(personName);
            var container = _currentSoftwareSystem.GetContainerWithName(containerName);
            person.Uses(container, desc);

            return this;
        }

        internal C4Builder SoftwareSystemUses(string sourceSoftwareSystemName, string destinationSoftwareSystemName, string desc) {
            var sourceSoftwareSystem = _workspace.Model.GetSoftwareSystemWithName(sourceSoftwareSystemName);
            var destinationSoftwareSystem = _workspace.Model.GetSoftwareSystemWithName(destinationSoftwareSystemName);
            sourceSoftwareSystem.Uses(destinationSoftwareSystem, desc);

            return this;
        }

        internal C4Builder ContainerUses(string sourceContainerName, string destinationContainerName, string desc, string technology) {
            var sourceContainer = _currentSoftwareSystem.GetContainerWithName(sourceContainerName);
            var destinationContainer = _currentSoftwareSystem.GetContainerWithName(destinationContainerName);

            sourceContainer.Uses(destinationContainer, desc, technology);

            return this;
        }

        internal C4Builder ContainerUsesSofwareSystem(string containerName, string softwareSytemName, string desc, string technology) {
            var container = _currentSoftwareSystem.GetContainerWithName(containerName);
            var softwareSystem = _workspace.Model.GetSoftwareSystemWithName(softwareSytemName);

            container.Uses(softwareSystem, desc, technology);

            return this;
        }

        internal C4Builder SoftwareSystemUsesContainer(string softwareSytemName, string containerName, string desc, string technology) {
            var container = _currentSoftwareSystem.GetContainerWithName(containerName);
            var softwareSystem = _workspace.Model.GetSoftwareSystemWithName(softwareSytemName);

            softwareSystem.Uses(container, desc, technology);

            return this;
        }

        internal C4Builder AddStyle(ElementStyle elementStyle) {
            _workspace.Views.Configuration.Styles.Add(elementStyle);

            return this;
        }

        internal C4Builder SoftwareSystemDelivers(string softwareSystemName, string personName, string desc) {
            var softwareSystem = _workspace.Model.GetSoftwareSystemWithName(softwareSystemName);
            var person = _workspace.Model.GetPersonWithName(personName);
            softwareSystem.Delivers(person, desc);

            return this;
        }

        internal SoftwareSystem GetSoftwareSystem(string softwareSystemName) {
            return _workspace.Model.GetSoftwareSystemWithName(softwareSystemName);
        }

        internal C4Builder AddSoftwareSystem(string name, string desc, string tag = null) {
            var softwareSystem = _workspace.Model.AddSoftwareSystem(name, desc);

            if (!string.IsNullOrEmpty(tag)) {
                softwareSystem.AddTags(tag);
            }

            return this;
        }
    }
}