using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Baas.Core.BlockchainDtos
{
    public class AccountFunctions
    {
        public partial class AddAccountFunction : AddAccountFunctionBase { }

        [Function("AddAccount", "uint256")]
        public class AddAccountFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "_idOffChain", 1)]
            public virtual BigInteger IdOffChain { get; set; }
            [Parameter("address", "_accountEth", 2)]
            public virtual string AccountEth { get; set; }
            [Parameter("bytes32", "_fieldOne", 3)]
            public virtual byte[] FieldOne { get; set; }
            [Parameter("bytes32", "_fieldTwo", 4)]
            public virtual byte[] FieldTwo { get; set; }
            [Parameter("bytes32", "_fieldThree", 5)]
            public virtual byte[] FieldThree { get; set; }
            [Parameter("uint8", "_status", 6)]
            public virtual byte Status { get; set; }
        }

        public partial class AccountsCountFunction : AccountsCountFunctionBase { }

        [Function("AccountsCount", "uint256")]
        public class AccountsCountFunctionBase : FunctionMessage
        {

        }

        public partial class GetAccountFunction : GetAccountFunctionBase { }

        [Function("GetAccount", typeof(GetAccountOutputDTO))]
        public class GetAccountFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "_idaccount", 1)]
            public virtual BigInteger Idaccount { get; set; }
        }

        public partial class GetAccountEthFunction : GetAccountEthFunctionBase { }

        [Function("GetAccountEth", typeof(GetAccountEthOutputDTO))]
        public class GetAccountEthFunctionBase : FunctionMessage
        {
            [Parameter("address", "_ethAccount", 1)]
            public virtual string EthAccount { get; set; }
        }


        public partial class GetAccountIdOffchainFunction : GetAccountIdOffchainFunctionBase { }

        [Function("GetAccountIdOffchain", typeof(GetAccountIdOffchainOutputDTO))]
        public class GetAccountIdOffchainFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "_idOffChain", 1)]
            public virtual BigInteger IdOffChain { get; set; }
        }

        public partial class NewAccountEventDTO : NewAccountEventDTOBase { }

        [Event("NewAccount")]
        public class NewAccountEventDTOBase : IEventDTO
        {
            [Parameter("uint256", "idBlockchain", 1, false)]
            public virtual BigInteger IdBlockchain { get; set; }
            [Parameter("uint256", "idDicio", 2, false)]
            public virtual BigInteger IdDicio { get; set; }
            [Parameter("address", "accountEth", 3, false)]
            public virtual string AccountEth { get; set; }
            [Parameter("bytes32", "fieldOne", 4, false)]
            public virtual byte[] FieldOne { get; set; }
            [Parameter("bytes32", "fieldTwo", 5, false)]
            public virtual byte[] FieldTwo { get; set; }
            [Parameter("bytes32", "fieldThree", 6, false)]
            public virtual byte[] FieldThree { get; set; }
            [Parameter("uint256", "datetime", 7, false)]
            public virtual BigInteger Datetime { get; set; }
            [Parameter("uint8", "status", 8, false)]
            public virtual byte Status { get; set; }
        }



        public partial class AccountsCountOutputDTO : AccountsCountOutputDTOBase { }

        [FunctionOutput]
        public class AccountsCountOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class GetAccountOutputDTO : GetAccountOutputDTOBase { }

        [FunctionOutput]
        public class GetAccountOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
            [Parameter("address", "", 2)]
            public virtual string ReturnValue2 { get; set; }
            [Parameter("bytes32", "", 3)]
            public new string ReturnValue3 { get; set; }
            [Parameter("bytes32", "", 4)]
            public new string ReturnValue4 { get; set; }
            [Parameter("bytes32", "", 5)]
            public new string ReturnValue5 { get; set; }
            [Parameter("uint256", "", 6)]
            public virtual BigInteger ReturnValue6 { get; set; }
            [Parameter("uint8", "", 7)]
            public virtual byte ReturnValue7 { get; set; }
        }
        public partial class GetAccountEthOutputDTO : GetAccountEthOutputDTOBase { }

        [FunctionOutput]
        public class GetAccountEthOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
            [Parameter("address", "", 2)]
            public virtual string ReturnValue2 { get; set; }
            [Parameter("bytes32", "", 3)]
            public new string ReturnValue3 { get; set; }
            [Parameter("bytes32", "", 4)]
            public new string ReturnValue4 { get; set; }
            [Parameter("bytes32", "", 5)]
            public new string ReturnValue5 { get; set; }
            [Parameter("uint256", "", 6)]
            public virtual BigInteger ReturnValue6 { get; set; }
            [Parameter("uint8", "", 7)]
            public virtual byte ReturnValue7 { get; set; }
        }


        public partial class GetAccountIdOffchainOutputDTO : GetAccountIdOffchainOutputDTOBase { }

        [FunctionOutput]
        public class GetAccountIdOffchainOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
            [Parameter("address", "", 2)]
            public virtual string ReturnValue2 { get; set; }
            [Parameter("bytes32", "", 3)]
            public new string ReturnValue3 { get; set; }
            [Parameter("bytes32", "", 4)]
            public new string ReturnValue4 { get; set; }
            [Parameter("bytes32", "", 5)]
            public new string ReturnValue5 { get; set; }
            [Parameter("uint256", "", 6)]
            public virtual BigInteger ReturnValue6 { get; set; }
            [Parameter("uint8", "", 7)]
            public virtual byte ReturnValue7 { get; set; }
        }
    }
}
