using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Collections.Generic;
using System.Numerics;

namespace Baas.Core.BlockchainDtos
{
    public partial class AddAttestationFunction : AddAttestationFunctionBase { }

    [Function("addAttestation", "uint256")]
    public class AddAttestationFunctionBase : FunctionMessage
    {
        [Parameter("address", "_userEth", 1)]
        public virtual string UserEth { get; set; }
        [Parameter("string", "_id", 2)]
        public virtual string Id { get; set; }
        [Parameter("string", "_value", 3)]
        public virtual string Value { get; set; }
        [Parameter("uint256", "_timeStamp", 4)]
        public virtual BigInteger TimeStamp { get; set; }
        [Parameter("uint8", "_status", 5)]
        public virtual byte Status { get; set; }
    }

    public partial class AddAttestorFunction : AddAttestorFunctionBase { }

    [Function("addAttestor")]
    public class AddAttestorFunctionBase : FunctionMessage
    {
        [Parameter("address", "_attestor", 1)]
        public virtual string Attestor { get; set; }
        [Parameter("bool", "_status", 2)]
        public virtual bool Status { get; set; }
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class AttestationsCountFunction : AttestationsCountFunctionBase { }

    [Function("attestationsCount", "uint256")]
    public class AttestationsCountFunctionBase : FunctionMessage
    {

    }

    public partial class GetAttestationsByIndexFunction : GetAttestationsByIndexFunctionBase { }

    [Function("getAttestationsByIndex", typeof(GetAttestationsByIndexOutputDTO))]
    public class GetAttestationsByIndexFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_indexAttestation", 1)]
        public virtual BigInteger IndexAttestation { get; set; }
    }

    public partial class GetAttestationsByUserFunction : GetAttestationsByUserFunctionBase { }

    [Function("getAttestationsByUser", "uint256[]")]
    public class GetAttestationsByUserFunctionBase : FunctionMessage
    {
        [Parameter("address", "_userEth", 1)]
        public virtual string UserEth { get; set; }
    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class QueryAttestorFunction : QueryAttestorFunctionBase { }

    [Function("queryAttestor", "bool")]
    public class QueryAttestorFunctionBase : FunctionMessage
    {
        [Parameter("address", "attestor", 1)]
        public virtual string Attestor { get; set; }
    }

    public partial class AttestorChangeEventDTO : AttestorChangeEventDTOBase { }

    [Event("AttestorChange")]
    public class AttestorChangeEventDTOBase : IEventDTO
    {
        [Parameter("address", "attestor", 1, false)]
        public virtual string Attestor { get; set; }
        [Parameter("address", "admin", 2, false)]
        public virtual string Admin { get; set; }
        [Parameter("bool", "status", 3, false)]
        public virtual bool Status { get; set; }
        [Parameter("uint256", "datetime", 4, false)]
        public virtual BigInteger Datetime { get; set; }
    }

    public partial class NewAttestationEventDTO : NewAttestationEventDTOBase { }

    [Event("NewAttestation")]
    public class NewAttestationEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "index", 1, false)]
        public virtual BigInteger Index { get; set; }
        [Parameter("address", "user", 2, false)]
        public virtual string User { get; set; }
        [Parameter("string", "value", 3, false)]
        public virtual string Value { get; set; }
        [Parameter("string", "id", 4, false)]
        public virtual string Id { get; set; }
        [Parameter("uint256", "timestamp", 5, false)]
        public virtual BigInteger Timestamp { get; set; }
        [Parameter("uint8", "status", 6, false)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "createDate", 7, false)]
        public virtual BigInteger CreateDate { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true)]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true)]
        public virtual string NewOwner { get; set; }
    }





    public partial class AttestationsCountOutputDTO : AttestationsCountOutputDTOBase { }

    [FunctionOutput]
    public class AttestationsCountOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetAttestationsByIndexOutputDTO : GetAttestationsByIndexOutputDTOBase { }

    [FunctionOutput]
    public class GetAttestationsByIndexOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("string", "", 2)]
        public virtual string ReturnValue2 { get; set; }
        [Parameter("uint256", "", 3)]
        public virtual BigInteger ReturnValue3 { get; set; }
        [Parameter("uint256", "", 4)]
        public virtual BigInteger ReturnValue4 { get; set; }
        [Parameter("uint8", "", 5)]
        public virtual byte ReturnValue5 { get; set; }
    }

    public partial class GetAttestationsByUserOutputDTO : GetAttestationsByUserOutputDTOBase { }

    [FunctionOutput]
    public class GetAttestationsByUserOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256[]", "", 1)]
        public virtual List<BigInteger> ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class QueryAttestorOutputDTO : QueryAttestorOutputDTOBase { }

    [FunctionOutput]
    public class QueryAttestorOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}