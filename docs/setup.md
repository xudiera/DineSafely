# Getting started

## System requirements
1. Docker installed locally [Docker Engine](https://docs.docker.com/engine/).
2. Install [Visual Studio Code](https://code.visualstudio.com/).
3. Install the [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers).
4. Clone the repository.
5. On the `.devcontainer` folder add a `.env` file with the following variables (the example values must be adjusted for the configuration of your setup):

> POSTGRES_DB=dinesafely  
> POSTGRES_USER=postgres  
> POSTGRES_PASSWORD=postgres

6. Start Visual Studio Code, run the **Dev Containers: Open Folder in Container...** command from the Command Palette.