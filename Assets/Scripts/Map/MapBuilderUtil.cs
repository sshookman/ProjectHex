using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Provides static functions to facilitate map building
/// </summary>
public class MapBuilderUtil {

	public static int BLOCK = 0;
	public static int TILE = 1;
	public static int HUMAN = 2;
	public static int GHOUL = 4;
	public static int SPAWN = 8;

	/// <summary>
	/// Generates a template for the forest map using integers to represent tile
	/// states
	/// </summary>
	/// <returns>int[,] - The forest map template</returns>
	/// <param name="width">map width</param>
	/// <param name="height">map height</param>
	public static int[,] GenerateForestMap(int width, int height) {

		//UnityEngine.Random.seed = seed.GetHashCode();
		int[,] template = new int[width, height];

		for (int w = 1; w < width-1; w++) {
			for (int h = 1; h < height-1; h++) {
				template[w, h] = (UnityEngine.Random.value > 0.5f) ? TILE : BLOCK;
			}
		}

		template = Refine(width, height, template, 1);
		template = AddHumanPlacement(width, height, template);
		template = AddGhoulPlacement(width, height, template);

		return template;
	}

	/// <summary>
	/// Refines the structure of the map template using cellular automata
	/// algorithm over a set number of iterations
	/// </summary>
	/// <returns>int[,] - The refined map template</returns>
	/// <param name="width">map width</param>
	/// <param name="height">map height</param>
	/// <param name="tiles">map template</param>
	/// <param name="iterations">number of iterations</param>
	private static int[,] Refine(int width, int height, int[,] template, int iterations) {
		int[,] refinedTemplate = new int[width, height];
		for (int i = 0; i < iterations; i++) {
			for (int w = 0; w < width; w++) {
				for (int h = 0; h < height; h++) {
					
					List<Vector2> neighbors = MapUtil.GetNeighbors(w, h);
					int openCount = (template[w, h] > 0) ? 1 : 0;

					neighbors.ForEach(delegate (Vector2 neighbor){
						if (neighbor.x >= 0 && neighbor.x < width && neighbor.y >= 0 && neighbor.y < height) {
							openCount += (template[(int)neighbor.x, (int)neighbor.y] >= TILE) ? 1 : 0;
						}
					});

					refinedTemplate[w, h] = (openCount >= 3) ? TILE : BLOCK;
				}
			}
			template = refinedTemplate;
		}

		return template;
	}

	/// <summary>
	/// Carves out a place on the map where humans can be placed
	/// </summary>
	/// <returns>int[,] - map template with human placement</returns>
	/// <param name="width">map width</param>
	/// <param name="height">map height</param>
	/// <param name="tiles">map template</param>
	private static int[,] AddHumanPlacement(int width, int height, int[,] template) {
		for (int w = 0; w < 5; w++) {
			for (int h = 0; h < 5; h++) {
				template[w, h] = TILE + HUMAN;
			}
		}

		return template;
	}

	/// <summary>
	/// Adds areas on the map where ghouls can be placed
	/// </summary>
	/// <returns>The ghoul placement int array</returns>
	/// <param name="width">Width int</param>
	/// <param name="height">Height int</param>
	/// <param name="tiles">Tiles int array</param>
	private static int[,] AddGhoulPlacement(int width, int height, int[,] tiles) {

		int spawnLimit = (width + height) / 20;
		int ghoulLimit = (width + height) / 2;

		while (ghoulLimit > 0) { 
			int w = UnityEngine.Random.Range(1, width);
			int h = UnityEngine.Random.Range(1, height);

			if (tiles[w, h] == TILE) {

				if (spawnLimit > 0) {
					tiles[w, h] += SPAWN;
					spawnLimit--;
				} else {				
					tiles[w, h] += GHOUL;
					ghoulLimit--;
				}

				List<Vector2> neighbors = MapUtil.GetNeighbors(w, h);
				neighbors.ForEach(delegate (Vector2 neighbor) {
					if (neighbor.x >= 0 && neighbor.x < width &&
						neighbor.y >= 0 && neighbor.y < width) {
						if (tiles[(int)neighbor.x, (int)neighbor.y] == TILE && UnityEngine.Random.value > 0.75f) {
							tiles[(int)neighbor.x, (int)neighbor.y] += GHOUL;
							ghoulLimit--;
						}
					}
				});
			}
		}

		return tiles;
	}
}