#!/bin/bash

pushd ../app/frontend && ng serve && popd
pushd ../app/backend/SampleWebApp && dotnet run && popd