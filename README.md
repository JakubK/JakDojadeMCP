# JakDojade MCP Server

My first attempt to MCP Server development.

MCP Server for consuming JakDojade [API](https://docs.jakdojade.pl/restxml/rest/)

## Tools

- `list-locations` - Works as search for detecting concrete locations such as stops by common name.

- `list-departures` - Allows to see schedule table contents for given stop.

- `find-route` - Finds route between 2 points given by their coordinates.

## Resources

- cities - resource providing data from static endpoint which holds JSON with all supported agglomerations and transport operators associated with them

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

### Example setup with mcp.json in vscode

```json
{
    "servers": {
        "my-mcp-server": {
            "type": "stdio",
            "command": "dotnet",
            "args": [
                "docker", "run", "--rm", "-it", "-e", "PublicKey=xyz", "-e", "SecretKey=xyz"
            ]
        }
    }
}
```