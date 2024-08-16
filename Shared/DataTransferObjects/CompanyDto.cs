namespace Shared.DataTransferObjects
{
    
    public record CompanyDto/*(Guid guid, string Name, string fullAddress)*/
    {
        public Guid guid { get; init; }
        public string Name { get; init; }
        public string fullAddress { get; init; }
    }
}
