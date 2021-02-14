/*
Author: Zachary Boehm
version: 11.25.2019
*/
#include "Character.h"
#include <exception>
#include <stdexcept>
using namespace std;
/*
This method will return the const read only version of the inventory
*/
const Collection<const Item>& Character::getInventory()
{
	Collection<const Item>& inventory{ this->Inventory };
	return inventory;
}
/*
This method will call addItem from Inventory.h to add an item
*/
void Character::addItem(const Item& item)
{
	this->Inventory.addItem(item);
}
/*
This method will call dropItem form Inventory.h to drop an item
*/
void Character::dropItem(const Item& item)
{
	this->Inventory.dropItem(item);
}
/*
This method will return the total weight of the inventory's weight and the equiped armor and weapon weight
*/
double Character::getTotalWeight() const
{
	return (Weight + this->Inventory.getTotalWeight());
}
/*
This method returns the currently Equiped armor in the specified slot or it will return nullptr if there isn't a armor equiped.
will throw a out_of_range if the slotID passed is out of the range of 0-5
*/
const Armor* Character::getEquippedArmor(unsigned int slotID) const
{
	if (0 <= slotID && slotID < 6)
	{
		//check for nullptr
		if (EquipedArmor.at(slotID))
		{
			return EquipedArmor[slotID];
		}
		else
		{
			return nullptr;
		}
		
	}
	else
	{
		throw out_of_range("Invalid slotID");
	}
}
/*
This method Will return the total armor rating of all of the equiped armor
*/
unsigned int Character::getTotalArmorRating() const
{
	return ArmorRating;
}
/*
This method Will equip an aromor
*/
void Character::equipArmor(const Armor& armor)
{
	const Item* tempItem = this->Inventory.findItem(dynamic_cast<const Item&>(armor));
	const Armor* tempArmor = dynamic_cast<const Armor*>(tempItem);
	//if the armor is in the inventory equip it else throw a logic error
	if (this->Inventory.find(dynamic_cast<const Item&>(armor)))
	{
		this->Inventory.dropItem(dynamic_cast<const Item&>(armor));
		// if there is an item in that slot already unequip it and equip the new one. Else just equip the armor
		if (EquipedArmor[armor.getSlotID()])
		{
			unequipArmor(armor.getSlotID());
			EquipedArmor[armor.getSlotID()] = dynamic_cast<const Armor*>(tempArmor);
		}
		else
		{
			EquipedArmor[armor.getSlotID()] = dynamic_cast<const Armor*>(tempArmor);
		}
		Weight += tempArmor->getWeight();
		ArmorRating += tempArmor->getRating();
	}
	else
	{
		throw logic_error("Armor not in inventory");
	}
}
/*
This method will unequip an armor piece from a given slot ID
*/
void Character::unequipArmor(unsigned int slotID)
{
	// check if in range and if not throw out_of_range
	if (0 <= slotID && slotID < 6)
	{
		// check if there is an item there
		if (getEquippedArmor(slotID))
		{
			//convert to a Item type then add to inventory
			const Armor& tempArmor = *(EquipedArmor[slotID]);
			const Item& temp = dynamic_cast<const Item&>(tempArmor);
			this->Inventory.addItem(temp);
			//update values
			Weight -= EquipedArmor[slotID]->getWeight();
			ArmorRating -= EquipedArmor[slotID]->getRating();
			//set current armor in that slot as nothing(nullptr)
			EquipedArmor[slotID] = nullptr;
		}
	}
	else
	{
		throw out_of_range("Invalid slotID");
	}
	
}
/*
This method will return the currently equiped weapon
*/
const Weapon* Character::getEquippedWeapon() const
{
	//check if there is an item in the slot, if not return nullptr
	if (EquipedWeapon[0] != nullptr)
	{
		//return the current weapon
		return EquipedWeapon[0];
	}
	else
	{
		return nullptr;
	}
}
/*
This method Will equip a given weapon into the Equiped weapon container
*/
void Character::equipWeapon(const Weapon& weapon)
{
	//check if the weapon is in the inventory
	if (this->Inventory.find(dynamic_cast<const Item&>(weapon)))
	{
		//convert the item from the inventory to a weapon type and temp store it
		const Item* tempItem = this->Inventory.findItem(dynamic_cast<const Item&>(weapon));
		const Weapon* tempWeapon = dynamic_cast<const Weapon*>(tempItem);
		//drop the item from the inventory
		this->Inventory.dropItem(*tempItem);
		//check if item is already in slot if so unequip and equip the new one
		if (EquipedWeapon[0] != nullptr)
		{
			unequipWeapon();
			EquipedWeapon[0] = tempWeapon;
		}
		else
		{
			//just equip the weapon
			EquipedWeapon[0] = tempWeapon;
		}
		//update values
		Weight += weapon.getWeight();
	}
	else
	{
		throw logic_error("Armor not in inventory");
	}
}
/*
This method will unequip the currently equiped weapon
*/
void Character::unequipWeapon()
{
	//check if there is an weapon in the slot
	if (EquipedWeapon[0] != nullptr)
	{
		//convert to a item type and add to inventory
		const Item& temp = dynamic_cast<const Item&>(*(EquipedWeapon[0]));
		this->Inventory.addItem(temp);
		//update values and set current equiped weapon to nothing(nullptr)
		Weight -= EquipedWeapon[0]->getWeight();
		EquipedWeapon[0] = nullptr;
	}
}
/*
This method will check if the maximum weight is above zero then call the optimize function from Inventory.h
*/
void Character::optimizeInventory(double maximumWeight)
{
	//check if in range
	if (maximumWeight < 0) 
	{
		throw out_of_range("maximumWeight is less than 0");
	}
	else
	{
		this->Inventory.optimizeInventory(maximumWeight, Weight);
	}
}
/*
This method Will call optimizeEquipmentArmor from Inventory.h for every armor slot to try to equip the best in slot for every slot.
Then it will call optimizeEquipmentWeapon from Inventory.h to find the best in slot weapon and equip it
*/
void Character::optimizeEquipment()
{
	for (int i = 0; i < Armor::SLOT_COUNT; i++)
	{
		//checks for nullptr(no item is better) before trying to equip anything.
		if (this->Inventory.optimizeEquipmentArmor(EquipedArmor[i], i))
		{
			//equips the item with the highest armor rating for that slot
			this->equipArmor(*(this->Inventory.optimizeEquipmentArmor(EquipedArmor[i], i)));
		}
	}
	//checks for nullptr(no item is better) before trying to equip anything.
	if (this->Inventory.optimizeEquipmentWeapon(EquipedWeapon[0]))
	{
		//equips the weapon with the highest damage
		this->equipWeapon(*(this->Inventory.optimizeEquipmentWeapon(EquipedWeapon[0])));
	}
}
