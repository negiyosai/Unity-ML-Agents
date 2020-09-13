# Unity ML Agents

A small project exploring the basic premise of reinforcement learning using Unity's ML agents. The project trains a game object to jump over obstacles at the correct time to score a point.

## Commands

### Installing ml-agents

1. Create a Virtual Environment:
    cd into an empty folder where you want to create the virtual environment
    ```
    py -m venv ExampleNameEnv
    ```
    To activate Virtual Environment, cd into virtual environment location and
    ```
    .\Scripts\Activate
    ```

2. Install ml-agents
    While the virtual environment is activated,
    ```
    pip install mlagents
    ```

3. Training Commands
    cd into yaml location inside the unity project
    ```
    mlagents-learn Jumper.yaml --run-id="JumperTestx"
    ```
    Tensorboard logs
    ```
    tensorboard --logdir=summaries
    ```
    Training a build.exe
    ```
    mlagents-learn Jumper.yaml --run-id=JumperTestx --env= ./Build/Unity-ML-Agents.app --time-scale=10 --quality-level=0 --width=512 --height=512
    ```

## Results

### Environment
![](/Readme_assets/Env.png)

### Losses
![](/Readme_assets/losses.png)

### Policies
![](/Readme_assets/Policies.png)

### Key
![](/Readme_assets/Key.png)

### Built With

* [Unity 2019.3.13f](https://unity.com/)
* [Unity ML agents](https://github.com/Unity-Technologies/ml-agents)