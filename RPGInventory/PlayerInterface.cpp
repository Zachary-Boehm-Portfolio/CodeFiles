#include "PlayerInterface.h"

PlayerInterface::PlayerInterface()
{
	input();
}

void PlayerInterface::input()
{
	int input = 7;
	std::cout << "============================" << std::endl;
	std::cout << "Welcome To Inventory Manager" << std::endl;
	do
	{
		//displays the options for the manager
		std::cout << "============================" << std::endl;
		std::cout << "Select an option:\n0: Print Inventory\n1 : Add Unequippable Item\n2 : Add Armor\n3 : Add Weapon\n4 : Optimize Inventory\n5 : Optimize Equipment\n6 : Clear Screen\n7 : Quit" << std::endl;
		std::cout << "============================" << std::endl;
		std::cin >> input;
		//checks for an option between 0 and 6 and keep asking until input is correct
		while (input < 0 || input > 7)
		{
			std::cout << "Incorrect input please try again!" << std::endl;
			std::cin >> input;
		}
		action(input);
	} while (input != 7);
}

void PlayerInterface::action(int input)
{
	switch (input)
	{
	case 0: printInventory();
		break;
	case 1: AddUnequippableItem();
		break;
	case 2: AddArmor();
		break;
	case 3: AddWeapon();
		break;
	case 4: OptimizeInventory();
		break;
	case 5: OptimizeEquipment();
		break;
	case 6: system("cls");
		break;
	}
}

void PlayerInterface::printInventory()
{
	if (Player_1.getTotalWeight() != 0)
	{
		if (Player_1.getTotalArmorRating() != 0)
		{
			std::cout << "Equiped Armor\n" << std::endl;
			for (int i = 0; i < 6; i++)
			{
				if (Player_1.getEquippedArmor(i))
				{
					printEquipedArmor(i);
				}
			}
			std::cout << std::endl << "Total AR: " << Player_1.getTotalArmorRating() << std::endl << std::endl;
		}

		if (Player_1.getEquippedWeapon() != nullptr)
		{
			std::cout << "Equiped Weapon\n" << std::left << std::setw(24) << Player_1.getEquippedWeapon()->getName() << "| " << Player_1.getEquippedWeapon()->getDamage() << " DMG | " << Player_1.getEquippedWeapon()->getGoldValue() << " GP | " << Player_1.getEquippedWeapon()->getWeight() << " lbs.\n\n";
		}
		if (Player_1.getInventory().getSize() != 0)
		{
			std::cout << std::left << std::setw(24) << "Item" << "|" << std::right << std::setw(9) << "Value   " << "|" << std::right << std::setw(18) << "Weight      " << "|" << std::right << std::setw(9) << "Ratio";
			std::cout << std::endl;
			Player_1.getInventory().forEach([](const Item& current)
				{
					try
					{
						const Weapon& weapon1 = dynamic_cast<const Weapon&>(current);
						std::cout << std::left << std::setw(24) << current.getName() << "|" << std::right << std::setw(9) << std::to_string(current.getGoldValue()) + " GP " << "|" << std::right << std::setw(18) << std::to_string(current.getWeight()) + " lbs. " << "|" << std::right << std::setw(9) << current.getGoldValue() / current.getWeight() << " | " << std::to_string(weapon1.getDamage()) + " DMG\n";
					}
					catch (std::bad_cast)
					{
						try
						{
							const Armor& armor1 = dynamic_cast<const Armor&>(current);
							std::cout << std::left << std::setw(24) << current.getName() << "|" << std::right << std::setw(9) << std::to_string(current.getGoldValue()) + " GP " << "|" << std::right << std::setw(18) << std::to_string(current.getWeight()) + " lbs. " << "|" << std::right << std::setw(9) << current.getGoldValue() / current.getWeight() << " | " << std::to_string(armor1.getRating()) + " AR\n";
						}
						catch (std::bad_cast)
						{
							std::cout << std::left << std::setw(24) << current.getName() << "|" << std::right << std::setw(9) << std::to_string(current.getGoldValue()) + " GP " << "|" << std::right << std::setw(18) << std::to_string(current.getWeight()) + " lbs." << "|" << std::right << std::setw(9) << current.getGoldValue() / current.getWeight() << std::endl;
						}
					}
				});
		}

		std::cout << std::endl << "Total Weight: " << Player_1.getTotalWeight() << " lbs.\n\n";
	}

}

void PlayerInterface::AddUnequippableItem()
{
	Item temp;
	int integer;
	std::string name;
	std::cout << "Enter the Name: ";
	std::cin >> name;
	temp.setName(name);
	std::cout << "Enter the Weight: ";
	std::cin >> integer;
	while (integer < 0)
	{
		std::cout << "please enter a number value." << std::endl;
		std::cin >> integer;
	}
	temp.setWeight(integer);
	std::cout << "Enter the Value: ";
	std::cin >> integer;
	while (integer < 0)
	{
		std::cout << "please enter a number value." << std::endl;
		std::cin >> integer;
	}
	temp.setGoldValue(integer);
	Player_1.addItem(temp);
}

void PlayerInterface::AddArmor()
{
	Armor temp;
	double weight;
	int slot, rating;
	unsigned int value;
	std::string name;
	//enter slotID
	std::cout << "Please enter the armor's slot: 0=chest, 1=legs, 2=hands, 3=feet, 4=helmet, 5=shield\n";
	std::cin >> slot;
	while (slot < 0 || slot > 5)
	{
		std::cout << "Please enter a number value." << std::endl;
		std::cin >> rating;
	}
	temp.setSlotID(slot);
	//enter name
	std::cout << "Enter the Name: ";
	std::cin >> name;
	temp.setName(name);
	//enter rating
	std::cout << "Enter the Rating: ";
	std::cin >> rating;
	while (rating < 0)
	{
		std::cout << "Please enter a number value." << std::endl;
		std::cin >> rating;
	}
	temp.setRating(rating);
	//enter weight
	std::cout << "Enter the Weight: ";
	std::cin >> weight;
	while (slot < 0)
	{
		std::cout << "Please enter a number value" << std::endl;
		std::cin >> weight;
	}
	temp.setWeight(weight);
	//enter value
	std::cout << "Enter the value(greater than 0): ";
	std::cin >> value;
	while (value < 0)
	{
		std::cout << "Please enter a integer greater than 0" << std::endl;
		std::cin >> value;
	}
	temp.setGoldValue(value);
	Player_1.addItem(temp);
}

void PlayerInterface::AddWeapon()
{
	Weapon temp;
	double weight;
	int damage;
	unsigned int value;
	std::string name;
	//enter name
	std::cout << "Enter the Name: ";
	std::cin >> name;
	temp.setName(name);
	//enter damage
	std::cout << "Enter the Damage: ";
	std::cin >> damage;
	while (damage < 0)
	{
		std::cout << "please enter an integer" << std::endl;
		std::cin >> damage;
	}
	temp.setDamage(damage);
	//enter weight
	std::cout << "Enter the Weight: ";
	std::cin >> weight;
	while (weight < 0)
	{
		std::cout << "Please enter a number value" << std::endl;
		std::cin >> weight;
	}
	temp.setWeight(weight);
	//enter value
	std::cout << "Enter the value(greater than 0): ";
	std::cin >> value;
	while (value < 0)
	{
		std::cout << "Please enter a integer greater than 0" << std::endl;
		std::cin >> value;
	}
	temp.setGoldValue(value);
	Player_1.addItem(temp);
}

void PlayerInterface::OptimizeInventory()
{
	double target;
	std::cout << "Enter target carry weight: ";
	std::cin >> target;
	while (target < 0)
	{
		std::cout << "Invalid Target try again." << std::endl;
		std::cin >> target;
	}
	Player_1.optimizeInventory(target);
	std::cout << "Total carry weight: " << Player_1.getTotalWeight() << " lbs.\n\n";
}

void PlayerInterface::OptimizeEquipment()
{
	Player_1.optimizeEquipment();
	if (Player_1.getTotalArmorRating() != 0)
	{
		std::cout << "Equiped Armor\n" << std::endl;
		for (int i = 0; i < 6; i++)
		{
			if (Player_1.getEquippedArmor(i))
			{
				printEquipedArmor(i);
			}
		}
		std::cout << std::endl << "Total AR: " << Player_1.getTotalArmorRating() << std::endl << std::endl;
	}
	if (Player_1.getEquippedWeapon())
	{
		std::cout << "Equiped Weapon\n" << std::left << std::setw(24) << Player_1.getEquippedWeapon()->getName() << "| " << Player_1.getEquippedWeapon()->getDamage() << " DMG | " << Player_1.getEquippedWeapon()->getGoldValue() << " GP | " << Player_1.getEquippedWeapon()->getWeight() << " lbs.\n\n";
	}
}

void PlayerInterface::printEquipedArmor(int input)
{
	switch (input)
	{
	case 0: std::cout << std::left << std::setw(9) << "0/Chest" << "|" << std::left << std::setw(14) << Player_1.getEquippedArmor(input)->getName() << "| " << Player_1.getEquippedArmor(input)->getRating() << " AR |" << std::right << std::setw(9) << std::to_string(Player_1.getEquippedArmor(input)->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(Player_1.getEquippedArmor(input)->getWeight()) + " lbs.\n";
		break;
	case 1: std::cout << std::left << std::setw(9) << "1/Legs" << "|" << std::left << std::setw(14) << Player_1.getEquippedArmor(input)->getName() << "| " << Player_1.getEquippedArmor(input)->getRating() << " AR |" << std::right << std::setw(9) << std::to_string(Player_1.getEquippedArmor(input)->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(Player_1.getEquippedArmor(input)->getWeight()) + " lbs.\n";
		break;
	case 2: std::cout << std::left << std::setw(9) << "2/Hands" << "|" << std::left << std::setw(14) << Player_1.getEquippedArmor(input)->getName() << "| " << Player_1.getEquippedArmor(input)->getRating() << " AR |" << std::right << std::setw(9) << std::to_string(Player_1.getEquippedArmor(input)->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(Player_1.getEquippedArmor(input)->getWeight()) + " lbs.\n";
		break;
	case 3: std::cout << std::left << std::setw(9) << "3/Feet" << "|" << std::left << std::setw(14) << Player_1.getEquippedArmor(input)->getName() << "| " << Player_1.getEquippedArmor(input)->getRating() << " AR |" << std::right << std::setw(9) << std::to_string(Player_1.getEquippedArmor(input)->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(Player_1.getEquippedArmor(input)->getWeight()) + " lbs.\n";
		break;
	case 4: std::cout << std::left << std::setw(9) << "4/Head" << "|" << std::left << std::setw(14) << Player_1.getEquippedArmor(input)->getName() << "| " << Player_1.getEquippedArmor(input)->getRating() << " AR |" << std::right << std::setw(9) << std::to_string(Player_1.getEquippedArmor(input)->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(Player_1.getEquippedArmor(input)->getWeight()) + " lbs.\n";
		break;
	case 5: std::cout << std::left << std::setw(9) << "5/Shield" << "|" << std::left << std::setw(14) << Player_1.getEquippedArmor(input)->getName() << "| " << Player_1.getEquippedArmor(input)->getRating() << " AR |" << std::right << std::setw(9) << std::to_string(Player_1.getEquippedArmor(input)->getGoldValue()) + " GP " << "|" << std::right << std::setw(11) << std::to_string(Player_1.getEquippedArmor(input)->getWeight()) + " lbs.\n";
		break;
	}
}

