using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This script manages all of the active components of the game such as 
/// the humans and ghouls on the map.
/// </summary>
public class GameManager : MonoBehaviour {

	private GhoulManager ghoulManager;
	private GameState currentState;
	private List<Actor> ghouls;
	private List<Actor> spawners;
	private List<Actor> humans;

	/// <summary>
	/// Initializes current game state to the human turn and initilaizes
	/// the list of Ghouls and Humans. 
	/// </summary>
	private void Start() {
		ghoulManager = GetComponent<GhoulManager>();
		currentState = GameState.LOADING;
		ghouls = new List<Actor>();
		spawners = new List<Actor>();
		humans = new List<Actor>();
	}

	/// <summary>
	/// During the human turn check to see if the turn is over by checking 
	/// if any humans have any options left
	/// </summary>
	private void Update() {

		if (currentState != GameState.LOADING && currentState != GameState.DEFEAT && currentState != GameState.VICTORY) {
			if (humans.Count == 0) {
				SetState(GameState.DEFEAT);
			}
			else if (ghouls.Count == 0) {
				SetState(GameState.VICTORY);
			}
		}

		if (currentState == GameState.HUMAN_TURN && humans != null) {
			bool isHumanTurnOver = true;
			humans.ForEach(delegate(Actor human) {
				isHumanTurnOver = (human.HasOptions()) ? false : isHumanTurnOver;
			});
			if (isHumanTurnOver) {
				SetState(GameState.GHOUL_TURN);
			}
		}
	}

	/// <summary>
	/// Getter for the current game state
	/// </summary>
	/// <returns>GameState - current game state</returns>
    public GameState GetState() {
        return currentState;
    }

	/// <summary>
	/// Setter for the current game state
	/// </summary>
	/// <param name="state">current game state</param>
    public void SetState(GameState state) {
        if (state != currentState) {

            currentState = state;
            switch (state) {
				case GameState.GHOUL_TURN:
					ghoulManager.StartGhoulTurn();
					return;
                default:
                    return;
            }
        }
    }

	/// <summary>
	/// Getter for the list of Ghouls
	/// </summary>
	/// <returns>List - Ghoul Actors</returns>
    public List<Actor> GetGhouls() {
        return ghouls;
    }

	/// <summary>
	/// Adds a ghoul to the list
	/// </summary>
	/// <param name="ghoul">Ghoul Actor</param>
	public void AddGhoul(Actor ghoul) {
		ghouls.Add(ghoul);
	}

	/// <summary>
	/// Getter for the list of Spawners
	/// </summary>
	/// <returns>List - Spawner Actors</returns>
	public List<Actor> GetSpawners() {
		return spawners;
	}

	/// <summary>
	/// Adds a spawner to the list
	/// </summary>
	/// <param name="ghoul">Spawner Actor</param>
	public void AddSpawner(Actor spawner) {
		spawners.Add(spawner);
	}

	/// <summary>
	/// Getter for the list of Humans
	/// </summary>
	/// <returns>List - Human Actors</returns>
    public List<Actor> GetHumans() {
        return humans;
    }

	/// <summary>
	/// Adds a human to the list
	/// </summary>
	/// <param name="human">Human Actor</param>
    public void AddHuman(Actor human) {
        humans.Add(human);
    }

	/// <summary>
	/// Removes an Actor from the appropriate list
	/// </summary>
	/// <param name="actor">Actor</param>
	public void Remove(Actor actor) {
		if (actor.GetComponent<AmblerAI>()) {
			ghouls.Remove(actor);
		} else if (actor.GetComponent<SpawnerAI>()) {
			spawners.Remove(actor);
		} else {
			humans.Remove(actor);
		}
	}

	/// <summary>
	/// Removes an Actor from the appropriate list
	/// </summary>
	/// <param name="actor">Actor GameObject</param>
	public void Remove(GameObject actor) {
		Remove(actor.GetComponent<Actor>());
	}
}
