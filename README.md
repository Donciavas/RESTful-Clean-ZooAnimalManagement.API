
    RESTful-Clean-ZooAnimalManagement.API

Almost done. Lacking unit tests and deciding if I should change database triggers into procedures, and call them directly. 

![ZooAnimalManagementSystemTask](https://github.com/Donciavas/RESTful-Clean-ZooAnimalManagement.API/assets/96888736/5672f09b-928c-4932-90fe-b6d4f9f0f2eb)

![image](https://github.com/Donciavas/RESTful-Clean-ZooAnimalManagement.API/assets/96888736/45018287-96f3-4eca-b1cb-5f10ea2d88e3)

Summary:

Objective:
Develop an Animal Transfer System in .NET for relocating animals to a new zoo, ensuring appropriate grouping based on specified rules and guidelines.

Features:

    1. Implement a mechanism for zookeepers to accommodate animals from input JSON to the new zoo.
    2. Consideration of available enclosures and adherence to grouping rules.
    3. Transfer rules include grouping vegetarian animals, keeping same species together, and handling meat-eating animals cautiously, grouping not more than 2 different species together.
    4. Input via REST API with JSON for animals and enclosures.
    5. Database storage of animal data with assigned enclosures.
    6. CRUD operations via REST API for new animal, enclosure addition/deletion.

Requirements:

    1. Library choices at developer's discretion.
    2. Use of relational database for data storage.
    3. Focus on good code design, OOP principles, unit tests, and adherence to .NET coding conventions.
    
Review Focus:

    1. Pay attention to OOP design, unit tests, and adherence to .NET coding conventions.
    2. Ensure proper handling of grouping rules during animal transfer.
    3. Implement secure and efficient REST API for data input and CRUD operations.

Data to input Example:

{
    "animals": [
        {
            "species": "Lion",
            "food": "Carnivore",
            "amount": 2
        },
        {
            "species": "Tiger",
            "food": "Carnivore",
            "amount": 3
        },
        {
            "species": "Elephant",
            "food": "Herbivore",
            "amount": 5
        },
        {
            "species": "Giraffe",
            "food": "Herbivore",
            "amount": 2
        },
        {
            "species": "Polar Bear",
            "food": "Carnivore",
            "amount": 2
        },
        {
            "species": "Zebra",
            "food": "Herbivore",
            "amount": 4
        },
        {
            "species": "Cheetah",
            "food": "Carnivore",
            "amount": 5
        },
        {
            "species": "Jaguar",
            "food": "Carnivore",
            "amount": 2
        },
        {
            "species": "Gorilla",
            "food": "Herbivore",
            "amount": 2
        },
        {
            "species": "Wolf",
            "food": "Carnivore",
            "amount": 4
        },
        {
            "species": "Hyena",
            "food": "Carnivore",
            "amount": 5
        }
    ],
"enclosures": [
        {
            "name": "Enclosure 1",
            "size": "Large",
            "location": "Outside",
            "objects": [
                "Rocks",
                "Logs",
                "Water Pond"
            ]
        },
        {
            "name": "Enclosure 2",
            "size": "Medium",
            "location": "Outside",
            "objects": [
                "Climbing Structures",
                "Shelter",
                "Pool"
            ]
        },
        {
            "name": "Enclosure 3",
            "size": "Huge",
            "location": "Outside",
            "objects": [
                "Trees",
                "Mud Bath Area",
                "Water Trough"
            ]
        },
        {
            "name": "Enclosure 4",
            "size": "Large",
            "location": "Outside",
            "objects": [
                "Tall Trees",
                "Feeding Platform",
                "Shade Structure"
            ]
        },
        {
            "name": "Enclosure 5",
            "size": "Medium",
            "location": "Inside",
            "objects": [
                "Tunnels",
                "Climbing Structures",
                "Enrichment Toys"
            ]
        },
        {
            "name": "Enclosure 6",
            "size": "Small",
            "location": "Inside",
            "objects": [
                "Nesting Boxes",
                "Perches",
                "Swing"
            ]
        }
    ]
}
