# AgentIsEasy

## Requirements

- .NET 10 SDK
- OpenAI API key
- GitHub Personal Access Token (for MCP demo parts)
- Internet connection

> Note: This project uses newer C# syntax (extension blocks), so .NET 10 is required.

## Setup & Run

Set environment variables:

```bash
OPENAI_API_KEY=your_openai_api_key
GITHUB_PAT=your_github_pat

git clone https://github.com/shakedAviad/AgentIsEasy.git
cd AgentIsEasy

dotnet restore
dotnet build
dotnet run
