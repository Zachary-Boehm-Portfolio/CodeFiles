/*
Author: Zachary Boehm
version: 11.25.2019
code used from given material
*/
#pragma once
#include "Item.h"
#include "Armor.h"
#include "Weapon.h"
#include "Collection.h"
#include "Inventory.h"
#include <array>
#include <list>
#include <vector>

class Character
{
public:
	Character() = default;
	Character(const Character& character) = delete;
	Character& operator = (const Character& character) = delete;

	const Collection<const Item>& getInventory();

	void addItem(const Item& item);
	void dropItem(const Item& item);
	double getTotalWeight() const;

	const Armor* getEquippedArmor(unsigned int slotID) const;
	unsigned int getTotalArmorRating() const;
	void equipArmor(const Armor& armor);
	void unequipArmor(unsigned int slotID);

	const Weapon* getEquippedWeapon() const;
	void equipWeapon(const Weapon& weapon);
	void unequipWeapon();

	void optimizeInventory(double maximumWeight);
	void optimizeEquipment();
private:
	// add your own private member variables and possibly some private member functions
	unsigned int ArmorRating{ 0 };
	double Weight{ 0.0 };
	Inventory<const Item> Inventory;
	std::vector<const Armor*> EquipedArmor{ nullptr,  nullptr,  nullptr,  nullptr,  nullptr,  nullptr , nullptr};
	std::vector<const Weapon*> EquipedWeapon{ nullptr };
};