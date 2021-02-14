/*
Author: Zachary Boehm
version: 11.25.2019
*/
#pragma once
#include "Collection.h"
#include "Armor.h"
#include "Weapon.h"
#include "Item.h"
#include<typeinfo>
#include <iostream>
#include <iomanip>
#include <list>
#include <exception>
#include <cctype>
// code in a better sorting system.
template<typename T>
class Inventory : public Collection<T>
{
public:
	virtual unsigned int getSize() const override;
	virtual void forEach(const std::function<void(const T&)>& accept)const override;

	virtual void forEach(const std::function<void(T&)>& accept) override;

	void addItem(const Item& item);
	void dropItem(const Item& item);
	double getTotalWeight() const;

	//will find the specific armor wanted for the purpose of moving it to equiped
	const Item* findItem(const Item& item);
	// will return true if the item is in the inventory
	bool find(const Item& item);
	//Will drop the items in inventory until the total weight is within the targeted range
	void optimizeInventory(double maximumWeight, double TotalWeight);
	//will return the armor pointer value so the character class can equip it
	const Armor* optimizeEquipmentArmor(const Armor* currentArmor, int slot);
	//will return the weapon pointer value so the character class can equip it
	const Weapon* optimizeEquipmentWeapon(const Weapon* currentWeapon);
private:
	std::list<const Item*> Inventory;
	std::list<Armor> armors;
	std::list<Weapon> weapons;
	std::list<Item> Items;
	double totalWeight{ 0.0 };
};
/*
This method will return the total size of the inventory
*/
template<typename T>
unsigned int Inventory<T>::getSize() const
{
	return this->Inventory.size();
}
/*
This method will apply a specific function to each element of the list in a const form
*/
template<typename T>
void Inventory<T>::forEach(const std::function<void(const T&)>& accept) const
{
	for (const T* item : this->Inventory)
	{
		const T& items = *item;
		accept(items);
	}
}
/*
This method will apply a specific function to each element which can be manipulated
*/
template<typename T>
void Inventory<T>::forEach(const std::function<void(T&)>& accept)
{
	for (T* item : this->Inventory)
	{
		T& items = *item;
		accept(items);
	}
}
/*
This method will add a item the the inventory and then sort the inventory based on the value to weight ratio
*/
template<typename T>
void Inventory<T>::addItem(const Item& item)
{
	// A try catch sequence to test for a Armor, Weapon, or Item
	try
	{
		const Armor& armor1 = dynamic_cast<const Armor&>(item);
		this->armors.emplace_back(armor1);
		this->Inventory.emplace_back(&armors.back());
	}
	catch (std::bad_cast)
	{
		try
		{
			const Weapon& weapon1 = dynamic_cast<const Weapon&>(item);
			this->weapons.emplace_back(weapon1);
			this->Inventory.emplace_back(&weapons.back());
		}
		catch (std::bad_cast)
		{
			this->Items.emplace_back(item);
			this->Inventory.emplace_back(&Items.back());
		}
	}
	//update weight
	totalWeight += item.getWeight();
	//sort inventory container
	this->Inventory.sort([](const Item* item1, const Item* item2) { return (item1->getWeight() / item1->getGoldValue()) < (item2->getWeight() / item2->getGoldValue()); });
}

template<typename T>
void Inventory<T>::dropItem(const Item& item)
{
	//call find to see if the item is in the inventory if not throw logic error
	if (find(item))
	{
		//finds the iterator location of the item being dropped
		std::list<const Item*>::iterator it = this->Inventory.begin();
		for (auto iterator = this->Inventory.begin(); iterator != this->Inventory.end(); iterator++)
		{
			if (*iterator == &item)
			{
				it = iterator;
			}
		}
		//uses iterator value to erase the element from the list
		this->Inventory.erase(it);
		//update weight
		totalWeight -= item.getWeight();
	}
	else
	{
		throw std::logic_error("Item not in Inventory");
	}

}
/*
This method returns the weight of the items only in the inventory
*/
template<typename T>
double Inventory<T>::getTotalWeight() const
{
	return totalWeight;
}
/*
This method will return the item pointer of the searched item. If the item is not found it will
return nullptr
*/
template<typename T>
const Item* Inventory<T>::findItem(const Item& item)
{
	for (std::list<const Item*>::iterator it = this->Inventory.begin(); it != this->Inventory.end(); it++)
	{
		const Item& temp = **it;
		if (&temp == &item)
		{
			return *it;
		}
	}
	return nullptr;
}
/*
This method returns true if the item is in the inventory container and false if not
*/
template<typename T>
bool Inventory<T>::find(const Item& item)
{
	for (std::list<const Item*>::iterator it = this->Inventory.begin(); it != this->Inventory.end(); it++)
	{
		const Item& temp = **it;
		if (&temp == &item)
		{
			return true;
		}
	}
	return false;
}
/*
This method will drop the items in order of value to weight ratio until it reaches the desired weight
*/
template<typename T>
void Inventory<T>::optimizeInventory(double maximumWeight, double Weight)
{
	//value that holds the total weight of equiped and inventory
	double totalW = totalWeight + Weight;
	//loop that will stop if the desired weight is met or the size of the inventory container reaches 0
	std::cout << "Dropped:\n";
	while (totalW > maximumWeight && this->Inventory.size() > 0)
	{
		// update the inventory's weight
		totalWeight -= this->Inventory.back()->getWeight();

		try
		{
			const Weapon& weapon1 = dynamic_cast<const Weapon&>(*this->Inventory.back());
			std::cout << std::left << std::setw(24) << this->Inventory.back()->getName() << "|" << std::right << std::setw(9) << std::to_string(this->Inventory.back()->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(this->Inventory.back()->getWeight()) + " lbs. " << "|" << std::right << std::setw(9) << this->Inventory.back()->getGoldValue() / this->Inventory.back()->getWeight() << " | " << std::to_string(weapon1.getDamage()) + " DMG\n";
		}
		catch (std::bad_cast)
		{
			try
			{
				const Armor& armor1 = dynamic_cast<const Armor&>(*this->Inventory.back());
				std::cout << std::left << std::setw(24) << this->Inventory.back()->getName() << "|" << std::right << std::setw(9) << std::to_string(this->Inventory.back()->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(this->Inventory.back()->getWeight()) + " lbs. " << "|" << std::right << std::setw(9) << this->Inventory.back()->getGoldValue() / this->Inventory.back()->getWeight() << " | " << std::to_string(armor1.getRating()) + " AR\n";
			}
			catch (std::bad_cast)
			{
				std::cout << std::left << std::setw(24) << this->Inventory.back()->getName() << "|" << std::right << std::setw(9) << std::to_string(this->Inventory.back()->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(this->Inventory.back()->getWeight()) + " lbs. " << "|" << std::right << std::setw(9) << this->Inventory.back()->getGoldValue() / this->Inventory.back()->getWeight() << std::endl;
			}
		}
		// remove the item from the end
		this->Inventory.pop_back();
		//update the total weight of equiped and inventory
		totalW = totalWeight + Weight;
	}
}
/*
This method will take in the current armor in a given slot and search through the inventory to find.
(1) that they have the same slot ID
(2) if the rating is higher then return the pointer of that item so it can be equiped
else it will return nullptr
*/
template<typename T>
const Armor* Inventory<T>::optimizeEquipmentArmor(const Armor* currentArmor, int slot)
{
	const Armor* returningArmor = nullptr;
	if (this->getSize() != 0)
	{
		this->forEach([&currentArmor, &returningArmor, &slot](const Item& current)
			{
				try
				{
					const Armor& tempArmor = dynamic_cast<const Armor&>(current);
					if (tempArmor.getSlotID() == slot)
					{
						if (currentArmor)
						{
							if (tempArmor.getRating() > currentArmor->getRating())
							{
								returningArmor = &tempArmor;
							}
						}
						else
						{
							returningArmor = &tempArmor;
						}
					}
				}
				catch (std::bad_cast) {}
			});
	}
	return returningArmor;
}
/*
This method takes in the currently equiped weapon and searches through the inventory to find another weapon and returns it if.
(1) The item has greater damage.
else it will return nullptr
*/
template<typename T>
const Weapon* Inventory<T>::optimizeEquipmentWeapon(const Weapon* currentWeapon)
{
	const Weapon* returningWeapon = nullptr;
	int totalDamage = 0;
	this->forEach([&currentWeapon, &returningWeapon, &totalDamage](const Item& current)
		{
			try
			{
				const Weapon& temp = dynamic_cast<const Weapon&>(current);
				if (currentWeapon)
				{
					if (temp.getDamage() > currentWeapon->getDamage())
					{
						returningWeapon = &temp;
					}
				}
				else
				{
					if (temp.getDamage() > totalDamage)
					{
						returningWeapon = &temp;
					}
				}
				
			}
			catch (std::bad_cast) {}
		});
	return returningWeapon;
}


