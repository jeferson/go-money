#!/bin/bash

# GoMoney .NET Client Build Script

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

echo "Building GoMoney .NET Client..."

# Restore packages
echo "Restoring packages..."
dotnet restore

# Build the solution
echo "Building solution..."
dotnet build --no-restore

# Run tests (when available)
if [ -d "tests" ]; then
    echo "Running tests..."
    dotnet test --no-build
fi

# Create NuGet package
echo "Creating NuGet package..."
dotnet pack src/GoMoney.Client/GoMoney.Client.csproj --configuration Debug -o packages/

echo "Build complete!"
echo "NuGet package created in packages/ directory"
echo ""
echo "To run the example:"
echo "  cd examples/ConsoleExample"
echo "  dotnet run"