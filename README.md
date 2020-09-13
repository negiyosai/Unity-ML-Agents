# Unity ML Agents

A small project exploring the basic premise of reinforcement learning using Unity's ML agents. The project trains a game object to jump over obstacles at the correct time to score a point.

## Commands

### Installing ml-agents

Create a Virtual Environment and install mlagents. Python is a prerequisite.

1. cd into an empty folder where you want to create the virtual environment  
    ```
    py -m venv ExampleNameEnv
    ```
2. To activate Virtual Environment, cd into virtual environment location and  
    ```
    .\Scripts\Activate
    ```
3. While the virtual environment is activated,
    ```
    pip install mlagents
    ```

### Training 
    
    cd into yaml location inside the unity project
    ```
    mlagents-learn Jumper.yaml --run-id="JumperTestx"
    ```
    Tensorboard logs

    Training a build.exe
    ```
    mlagents-learn Jumper.yaml --run-id=JumperTestx --env= ./Build/Unity-ML-Agents.app --time-scale=10 --quality-level=0 --width=512 --height=512
    ```

### Tensorboard Logs

    cd into location where "results" are stored
    ```
    tensorboard --logdir=summaries
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