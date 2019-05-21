# SmartContractFunction
An Azure Function to call Ethereum Smart Contracts

## Info
| | |
| --- | --- |
| ***Quality*** | &nbsp; |
| &nbsp;&nbsp;**Build Azure** | [![Build Status Azure](https://stef.visualstudio.com/SmartContractFunction/_apis/build/status/SmartContractFunction)](https://stef.visualstudio.com/SmartContractFunction/_build/latest?definitionId=23) |

## Functionality

### RunQueryFunctionAsync

Use this to call a *normal* function.

#### Example
Do a **POST** request with this body to endpoint <http://localhost:7071/api/QuerySmartContractFunction>

To call a function without parameters:
``` js
{
    "Endpoint": "https://ropsten.infura.io/v3/***",
    "ContractABI": "[{\"constant\":true,\"inputs\":[],\"name\":\"getVersion\",\"outputs\":[{\"name\":\"version\",\"type\":\"int256\"},{\"name\":\"description\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"setNumber\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"value\",\"type\":\"string\"}],\"name\":\"setString\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getString\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"number1\",\"type\":\"uint256\"},{\"name\":\"number2\",\"type\":\"uint256\"}],\"name\":\"addNumbers\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"pure\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getNumber\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"name\":\"version\",\"type\":\"int256\"},{\"name\":\"description\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
    "ContractAddress": "0x***",
    "CallerPrivateKey": "***",
    "FunctionName": "getString",
    "FromAddress": "0x***"
}
```

To call a function with parameters:
``` js
{
    "Endpoint": "https://ropsten.infura.io/v3/***",
    "ContractABI": "[{\"constant\":true,\"inputs\":[],\"name\":\"getVersion\",\"outputs\":[{\"name\":\"version\",\"type\":\"int256\"},{\"name\":\"description\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"setNumber\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"value\",\"type\":\"string\"}],\"name\":\"setString\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getString\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"number1\",\"type\":\"uint256\"},{\"name\":\"number2\",\"type\":\"uint256\"}],\"name\":\"addNumbers\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"pure\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getNumber\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"name\":\"version\",\"type\":\"int256\"},{\"name\":\"description\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
    "ContractAddress": "0x***",
    "CallerPrivateKey": "***",
    "FromAddress": "0x**",
    "FunctionName": "addNumbers",
    "FunctionParameters": [1, 4]
}
```

### RunExecuteFunctionAsync

Use this to execute a *transactional* function.

#### Example
Do a **POST** request with this body to endpoint <http://localhost:7071/api/ExecuteSmartContractFunction>

To call a function with parameters:
``` js
{
    "Endpoint": "https://ropsten.infura.io/v3/***",
    "ContractABI": "[{\"constant\":true,\"inputs\":[],\"name\":\"getVersion\",\"outputs\":[{\"name\":\"version\",\"type\":\"int256\"},{\"name\":\"description\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"setNumber\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"value\",\"type\":\"string\"}],\"name\":\"setString\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getString\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"number1\",\"type\":\"uint256\"},{\"name\":\"number2\",\"type\":\"uint256\"}],\"name\":\"addNumbers\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"pure\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getNumber\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"name\":\"version\",\"type\":\"int256\"},{\"name\":\"description\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
    "ContractAddress": "0x***",
    "CallerPrivateKey": "***",
    "FunctionName": "setString",
    "FromAddress": "0x**",
    "FunctionParameters": ["stef-1234"]
}
```