# Set the base image as the .NET 6.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
COPY . ./
RUN dotnet publish ./OData.Inspector/OData.Inspector.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="Paul Odero <paulodero@gmail.com>"
LABEL repository="https://github.com/paulodero/OData-Inspector"
LABEL homepage="https://github.com/paulodero/OData-Inspector"

# Label as GitHub action
LABEL com.github.actions.name=".NET schema inspector"
LABEL com.github.actions.description="A Github action that performs OData schema validations in comparison with another base schema."
LABEL com.github.actions.icon="sliders"
LABEL com.github.actions.color="purple"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY --from=build-env /out .
ENTRYPOINT [ "dotnet", "/OData.Inspector.dll" ]
