#!/bin/bash

echo "Building Cool Retro Terminal - Cross-platform build"
echo "=================================================="
echo ""

# Check for .NET CLI
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET CLI not found. Please install .NET 8.0 or later."
    echo "See: https://dotnet.microsoft.com/download"
    exit 1
fi

# Check .NET version
DOTNET_VERSION=$(dotnet --version)
echo "Using .NET version: $DOTNET_VERSION"

if [ ! -f "src/CoolRetroTerm/CoolRetroTerm.csproj" ]; then
    echo "Error: Project file not found. Make sure you're in the repository root directory."
    exit 1
fi

echo "Restoring packages..."
dotnet restore src/CoolRetroTerm/CoolRetroTerm.csproj
if [ $? -ne 0 ]; then
    echo "Failed to restore packages."
    exit 1
fi

echo ""
echo "Building application..."
dotnet build src/CoolRetroTerm/CoolRetroTerm.csproj -c Release --nologo
if [ $? -ne 0 ]; then
    echo "Failed to build application."
    exit 1
fi

echo ""
if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
    echo "Publishing Windows WPF application..."
    dotnet publish src/CoolRetroTerm/CoolRetroTerm.csproj -c Release -r win-x64 --self-contained -o dist/
    if [ $? -eq 0 ]; then
        echo ""
        echo "===================================================="
        echo "Build completed successfully!"
        echo ""
        echo "Executable location: dist/CoolRetroTerm.exe"
        echo ""
        echo "To run the application:"
        echo "  dist/CoolRetroTerm.exe"
        echo ""
        echo "For fullscreen mode:"
        echo "  dist/CoolRetroTerm.exe --fullscreen"
        echo "===================================================="
    else
        echo "Failed to publish Windows application."
        exit 1
    fi
else
    echo "Cross-platform build completed."
    echo ""
    echo "Note: This project is designed for Windows with WPF support."
    echo "On non-Windows platforms, only the console fallback is available."
    echo ""
    echo "To run the console version:"
    echo "  dotnet run --project src/CoolRetroTerm/CoolRetroTerm.csproj"
    echo ""
    echo "For the full WPF experience, build and run on Windows."
fi