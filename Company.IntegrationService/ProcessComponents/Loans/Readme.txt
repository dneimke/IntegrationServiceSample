

http://martinfowler.com/eaaCatalog/mapper.html
Mappers provide a way to have communications between two subsystems that still need to stay ignorant of each other.

In the case of this appilication, the Mapping layer provides the mapping that allows us to separate the IntegrationService from any 
domain objects that are exposed through the BusinessLayer.  This is done by sharing the data and message contracts between the business layer 
and the Services layer.

The pattern for communication layer and the business layer is established via the IProcessRequestComponent interface which takes a 
Message contract in, and returns a processed result as a Message contract.