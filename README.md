# JakDojade MCP Server

My first attempt to MCP Server development.
MCP Server for consuming JakDojade [API](https://docs.jakdojade.pl/restxml/rest/)

It can be used to grant your LLM/Agent powers to answer questions regarding best routes (including special requirements like preferred means of transport, time and date), provide schedule tables, all using natural language.

When combined with some external geolocation tool, it could provide even more accurate routing suggestions.

## Tools

- `list-locations` - Works as search for detecting concrete locations such as stops by common name.

- `list-departures` - Allows to see schedule table contents for given stop.

- `list-cities` - Lists all supported cities, and operators associated with them

- `find-route` - Finds route between 2 points given by their coordinates.

## Resources

No resources because vscode didnt want to use them even though they were accessible - everything moved to tools.

## Prompts

- `find_route` - Contains detailed instruction for LLM on how to obtain params for find-route

- `query_scheudle` - Contains detailed instruction for LLM on how to obtain params for list-departures

## Docker

### Building

```bash
docker build -t jd-mcp .
```

### Running

```bash
docker run --rm -it jd-mcp
```

Server by default runs in STDIO transport mode. In order to change this behaviour to HTTP, you need to override `TransportMode` env variable with anything else.

To access all API features, you should provide JakDojade API credentials

```bash
docker run --rm -it -e PublicKey=xyz -e SecretKey=xyz jd-mcp
```

#### Example setup with mcp.json in vscode with MCP in Docker

```json
{
    "servers": {
        "my-mcp-server": {
            "type": "stdio",
            "command": "docker",
            "args": [
                "run", "--rm", "-i", "-e", "PublicKey=xyz", "-e", "SecretKey=xyz", "jd-mcp"
            ]
        }
    }
}
```

#### Example setup with mcp.json in vscode with MCP source code

```json
{
    "servers": {
        "my-mcp-server": {
            "type": "stdio",
            "command": "dotnet",
            "args": [
                "run", "--project", "JakDojadeMCP.Server/JakDojadeMCP.Server.csproj",
            ]
        }
    }
}
```