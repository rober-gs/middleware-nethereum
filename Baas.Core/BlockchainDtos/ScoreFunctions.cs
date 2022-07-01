using System.Collections.Generic;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Baas.Core.BlockchainDtos
{
    public class ScoreFunctions
    {

        [Parameter("address", "swapAddress", 1)]
        public virtual string SwapAddress { get; set; }
    }

    public partial class CreateBroadcastFunction : CreateBroadcastFunctionBase { }

    [Function("createBroadcast")]
    public class CreateBroadcastFunctionBase : FunctionMessage
    {
        [Parameter("string", "keySearch", 1)]
        public virtual string KeySearch { get; set; }
        [Parameter("uint256", "embbeding", 2)]
        public virtual BigInteger Embbeding { get; set; }
    }

    public partial class ItExistFunction : ItExistFunctionBase { }

    [Function("itExist", typeof(ItExistOutputDTO))]
    public class ItExistFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "key", 1)]
        public virtual byte[] Key { get; set; }
    }

    public partial class PostRecordFunction : PostRecordFunctionBase { }

    [Function("postRecord", "bool")]
    public class PostRecordFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "broadcastID", 1)]
        public virtual BigInteger BroadcastID { get; set; }
        [Parameter("uint256", "quality", 2)]
        public virtual BigInteger Quality { get; set; }
        [Parameter("uint256", "score", 3)]
        public virtual BigInteger Score { get; set; }
        [Parameter("string", "uri", 4)]
        public virtual string Uri { get; set; }
        [Parameter("uint256", "captureDate", 5)]
        public virtual BigInteger CaptureDate { get; set; }
    }

    public partial class GetRecordFunction : GetRecordFunctionBase { }

    [Function("getRecord", typeof(GetRecordOutputDTO))]
    public class GetRecordFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
    }

    public partial class ParticipantsFunction : ParticipantsFunctionBase { }

    [Function("participants", "uint8")]
    public class ParticipantsFunctionBase : FunctionMessage
    {

    }

    public partial class ValidityFunction : ValidityFunctionBase { }

    [Function("validity", "uint256")]
    public class ValidityFunctionBase : FunctionMessage
    {

    }

    public partial class LogChangeValidityEventDTO : LogChangeValidityEventDTOBase { }

    [Event("LogChangeValidity")]
    public class LogChangeValidityEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, false)]
        public virtual string Account { get; set; }
        [Parameter("uint256", "newValidity", 2, false)]
        public virtual BigInteger NewValidity { get; set; }
    }

    public partial class LogCloseBroadcastEventDTO : LogCloseBroadcastEventDTOBase { }

    [Event("LogCloseBroadcast")]
    public class LogCloseBroadcastEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "broadcastID", 1, false)]
        public virtual BigInteger BroadcastID { get; set; }
        [Parameter("uint256", "scoreFinal", 2, false)]
        public virtual BigInteger ScoreFinal { get; set; }
    }

    public partial class LogPaticipationEventDTO : LogPaticipationEventDTOBase { }

    [Event("LogPaticipation")]
    public class LogPaticipationEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "broadcastID", 1, false)]
        public virtual BigInteger BroadcastID { get; set; }
        [Parameter("address", "participant", 2, false)]
        public virtual string Participant { get; set; }
    }

    public partial class NewBroadCastEventDTO : NewBroadCastEventDTOBase { }

    [Event("NewBroadCast")]
    public class NewBroadCastEventDTOBase : IEventDTO
    {
        [Parameter("address", "ownerSearch", 1, false)]
        public virtual string OwnerSearch { get; set; }
        [Parameter("uint256", "broadcastID", 2, false)]
        public virtual BigInteger BroadcastID { get; set; }
        [Parameter("string", "keySearch", 3, false)]
        public virtual string KeySearch { get; set; }
        [Parameter("uint256", "embedding", 4, false)]
        public virtual BigInteger Embedding { get; set; }
    }



    public partial class ItExistOutputDTO : ItExistOutputDTOBase { }

    [FunctionOutput]
    public class ItExistOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }



    public partial class GetRecordOutputDTO : GetRecordOutputDTOBase { }

    [FunctionOutput]
    public class GetRecordOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address[]", "relParticipants", 1)]
        public virtual List<string> RelParticipants { get; set; }
        [Parameter("uint256[]", "relScores", 2)]
        public virtual List<BigInteger> RelScores { get; set; }
        [Parameter("uint256[]", "relQuality", 3)]
        public virtual List<BigInteger> RelQuality { get; set; }
        [Parameter("uint256", "finalScore", 4)]
        public virtual BigInteger FinalScore { get; set; }
    }

    public partial class ParticipantsOutputDTO : ParticipantsOutputDTOBase { }

    [FunctionOutput]
    public class ParticipantsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class ValidityOutputDTO : ValidityOutputDTOBase { }

    [FunctionOutput]
    public class ValidityOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}