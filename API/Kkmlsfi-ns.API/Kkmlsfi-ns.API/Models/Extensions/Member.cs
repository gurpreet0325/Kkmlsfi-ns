namespace Kkmlsfi_ns.API.Models.Domain
{
    public partial class Member
    {
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleName))
                {
                    return $"{LastName}, {FirstName}";
                }
                else
                {
                    return $"{LastName}, {FirstName} {MiddleName}";
                }
            }
            
        }
    }
}
