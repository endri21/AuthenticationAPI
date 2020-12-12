

namespace CoreApiRegister.Features.Companies.Models
{
    public class BoolUpdateResponseModel
    {
        public bool Updated { get; set; } = false;
        public bool Allowed { get; set; } = false;
        public bool Success { get; set; } = false;
    }

    public class BoolDeleteResponseModel
    {
        public bool Deleted { get; set; } = false;
        public bool Allowed { get; set; } = false;
        public bool Success { get; set; } = false;
    }
}
