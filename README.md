# tenz

Privacy-preserved LLM-powered mobile chatbot.

# Getting started

## Development

This development setup was tested macOS Sonoma 14.6 (ARM chip).

1. Install .NET SDK by downloading and running the approprate installer from [here](https://dotnet.microsoft.com/en-us/download), or just run the following commands:
```
wget https://download.visualstudio.microsoft.com/download/pr/b5dfd4eb-19f4-4ba5-9a0c-50af354aa434/3f307be41112d4a8de659535e8badff2/dotnet-sdk-9.0.200-osx-arm64.pkg
sudo installer -pkg dotnet-sdk-9.0.200-osx-arm64.pkg -target /
rm dotnet-sdk-9.0.200-osx-arm64.pkg
```

2. Install .NET MAUI workload (you might need to open a new terminal first):
```
sudo $(which dotnet) workload install maui
```

3. Build app:
```
dotnet build tenz/
```

4. Run Mac Catalyst app:
```
dotnet run --project tenz/ --framework net9.0-maccatalyst
```


# License

