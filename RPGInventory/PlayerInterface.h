#pragma once
#include <iostream>
#include <iomanip>
#include "Character.h"
/*
=====================================
Welcome to
RPG Inventory Manager!
=====================================
Select an option:
0: Print Inventory
1: Add Unequippable Item
2: Add Armor
3: Add Weapon
4: Optimize Inventory
5: Optimize Equipment
6: Quit
=====================================
*/
class PlayerInterface
{
public:
	PlayerInterface();
	void input();
	void action(int input);
	void printInventory();
	void printEquipedArmor(int input);
	void AddUnequippableItem();
	void AddArmor();
	void AddWeapon();
	void OptimizeInventory();
	void OptimizeEquipment();
private:
	Character Player_1;
};