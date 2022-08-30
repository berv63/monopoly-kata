#!/bin/bash
set -a
source env/local/secrets.env
docker-compose -f docker-compose.yml -f env/local/docker-compose.local.yml up --build

# This script starts a local container build. This is the opposite of _down.sh