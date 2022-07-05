using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Baas.Core.BlockchainDtos
{
    public class SearchFunctions
    {
        public partial class ChangeQuorumFunction : ChangeQuorumFunctionBase { }

        [Function("ChangeQuorum", "bool")]
        public class ChangeQuorumFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "_quorum", 1)]
            public virtual BigInteger Quorum { get; set; }
        }

        public partial class CreateSearchFunction : CreateSearchFunctionBase { }

        [Function("CreateSearch", "bool")]
        public class CreateSearchFunctionBase : FunctionMessage
        {
            [Parameter("string", "_uuid", 1)]
            public virtual string Uuid { get; set; }
            [Parameter("string", "_curp", 2)]
            public virtual string Curp { get; set; }
            [Parameter("string", "_cid", 3)]
            public virtual string Cid { get; set; }
        }

        public partial class DetailParticipationFunction : DetailParticipationFunctionBase { }

        [Function("DetailParticipation", typeof(DetailParticipationOutputDTO))]
        public class DetailParticipationFunctionBase : FunctionMessage
        {
            [Parameter("string", "_uuid", 1)]
            public virtual string Uuid { get; set; }
            [Parameter("uint256", "index", 2)]
            public virtual BigInteger Index { get; set; }
        }

        public partial class DetailSearchFunction : DetailSearchFunctionBase { }

        [Function("DetailSearch", typeof(DetailSearchOutputDTO))]
        public class DetailSearchFunctionBase : FunctionMessage
        {
            [Parameter("string", "_uuid", 1)]
            public virtual string Uuid { get; set; }
        }

        public partial class ParticipateFunction : ParticipateFunctionBase { }

        [Function("Participate", "uint256")]
        public class ParticipateFunctionBase : FunctionMessage
        {
            [Parameter("string", "_uuid", 1)]
            public virtual string Uuid { get; set; }
            [Parameter("string", "_cid", 2)]
            public virtual string Cid { get; set; }
        }

        public partial class OwnerContractFunction : OwnerContractFunctionBase { }

        [Function("ownerContract", "address")]
        public class OwnerContractFunctionBase : FunctionMessage
        {

        }

        public partial class QuorumFunction : QuorumFunctionBase { }

        [Function("quorum", "uint256")]
        public class QuorumFunctionBase : FunctionMessage
        {

        }

        public partial class SetOwnerFunction : SetOwnerFunctionBase { }

        [Function("setOwner")]
        public class SetOwnerFunctionBase : FunctionMessage
        {
            [Parameter("address", "_newOwner", 1)]
            public virtual string NewOwner { get; set; }
        }

        public partial class BroadcastEndingEventEventDTO : BroadcastEndingEventEventDTOBase { }

        [Event("BroadcastEndingEvent")]
        public class BroadcastEndingEventEventDTOBase : IEventDTO
        {
            [Parameter("string", "uuid", 1, false)]
            public virtual string Uuid { get; set; }
        }

        public partial class BroadcastEventEventDTO : BroadcastEventEventDTOBase { }

        [Event("BroadcastEvent")]
        public class BroadcastEventEventDTOBase : IEventDTO
        {
            [Parameter("address", "owner", 1, false)]
            public virtual string Owner { get; set; }
            [Parameter("string", "uuid", 2, false)]
            public virtual string Uuid { get; set; }
            [Parameter("string", "cid", 3, false)]
            public virtual string Cid { get; set; }
            [Parameter("string", "curp", 4, false)]
            public virtual string Curp { get; set; }
        }

        public partial class CreatedSearchEventEventDTO : CreatedSearchEventEventDTOBase { }

        [Event("CreatedSearchEvent")]
        public class CreatedSearchEventEventDTOBase : IEventDTO
        {
            [Parameter("address", "owner", 1, false)]
            public virtual string Owner { get; set; }
            [Parameter("string", "uuid", 2, false)]
            public virtual string Uuid { get; set; }
            [Parameter("string", "cid", 3, false)]
            public virtual string Cid { get; set; }
        }

        public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

        [Event("OwnershipTransferred")]
        public class OwnershipTransferredEventDTOBase : IEventDTO
        {
            [Parameter("address", "previousOwner", 1, false)]
            public virtual string PreviousOwner { get; set; }
            [Parameter("address", "newOwner", 2, false)]
            public virtual string NewOwner { get; set; }
        }

        public partial class ParticipateEventEventDTO : ParticipateEventEventDTOBase { }

        [Event("ParticipateEvent")]
        public class ParticipateEventEventDTOBase : IEventDTO
        {
            [Parameter("address", "participant", 1, false)]
            public virtual string Participant { get; set; }
            [Parameter("string", "uuid", 2, false)]
            public virtual string Uuid { get; set; }
            [Parameter("string", "cid", 3, false)]
            public virtual string Cid { get; set; }
            [Parameter("uint256", "index", 4, false)]
            public virtual BigInteger Index { get; set; }
        }





        public partial class DetailParticipationOutputDTO : DetailParticipationOutputDTOBase { }

        [FunctionOutput]
        public class DetailParticipationOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "participant", 1)]
            public virtual string Participant { get; set; }
            [Parameter("string", "cid", 2)]
            public virtual string Cid { get; set; }
            [Parameter("uint256", "recordDate", 3)]
            public virtual BigInteger RecordDate { get; set; }
        }

        public partial class DetailSearchOutputDTO : DetailSearchOutputDTOBase { }

        [FunctionOutput]
        public class DetailSearchOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "owner", 1)]
            public virtual string Owner { get; set; }
            [Parameter("string", "uuid", 2)]
            public virtual string Uuid { get; set; }
            [Parameter("string", "curp", 3)]
            public virtual string Curp { get; set; }
            [Parameter("string", "cid", 4)]
            public virtual string Cid { get; set; }
            [Parameter("uint256", "createDate", 5)]
            public virtual BigInteger CreateDate { get; set; }
            [Parameter("uint8", "state", 6)]
            public virtual byte State { get; set; }
            [Parameter("uint256", "records", 7)]
            public virtual BigInteger Records { get; set; }
            [Parameter("uint256", "endDate", 8)]
            public virtual BigInteger EndDate { get; set; }
        }



        public partial class OwnerContractOutputDTO : OwnerContractOutputDTOBase { }

        [FunctionOutput]
        public class OwnerContractOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class QuorumOutputDTO : QuorumOutputDTOBase { }

        [FunctionOutput]
        public class QuorumOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }
    }
}