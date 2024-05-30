# Dictionary
This repository contains a C# WPF application that functions as an explanatory dictionary. The application has three main modules:
1. Administrative
2. Word Search
3. Entertainment

## Table of Contents

- [Features](#features)
- [Usage](#usage)
- [Modules](#modules)
  - [Administrative Module](#administrative-module)
  - [Word Search Module](#word-search-module)
  - [Entertainment Module](#entertainment-module)
- [Data Storage](#data-storage)
- [Screenshots](#screenshots)

## Features

- Add, modify, and delete words with associated descriptions, categories, and optional images.
- Search for words by category or without considering the category.
- An entertainment module with a guessing game where users guess words based on descriptions or images.
- User authentication for accessing the administrative module.

## Usage

1. Run the application.
2. Use the login screen to access the administrative module (default credentials can be found in the data file).
3. Navigate through the different modules using the provided menu.

## Modules

### Administrative Module

- **Login Required**: The user must log in using predefined credentials stored in a text file.
- **Features**:
  - Add a new word with a description, category, and optional image.
  - Modify existing words.
  - Delete words from the dictionary.
  - Validate input fields for completeness and correctness.
  
### Word Search Module

- **Search by Category**: Users can select a category and search for words within that category.
- **Autocomplete**: A textbox with an autocomplete feature that suggests words as the user types.
- **Display Details**: Upon selecting a word, the application displays the word's description, category, and associated image (if any).

### Entertainment Module

- **Guessing Game**: Users must guess five words from the dictionary based on descriptions or images.
- **Random Selection**: Words and hints (description or image) are chosen randomly.

## Data Storage

- **User Accounts**: Stored in a text file (`users.txt`) with predefined usernames and passwords.
- **Dictionary Data**: Stored in a text file (`dictionary.txt`), which contains words, descriptions, categories, and image paths.

  ## Screenshots
  ![image](https://github.com/RalucaSpt/Dictionary/assets/147080664/f2a62d40-bba3-491e-8f0a-cabcb7cbbaf7)
  ![image](https://github.com/RalucaSpt/Dictionary/assets/147080664/7362fd0f-d54b-4fd4-a541-eb0ba313ecb1)
![image](https://github.com/RalucaSpt/Dictionary/assets/147080664/391f712c-9f48-4e37-8543-cd27ded4455d)
![image](https://github.com/RalucaSpt/Dictionary/assets/147080664/9039c417-cd2f-4015-8f89-97b2f267bffa)
![image](https://github.com/RalucaSpt/Dictionary/assets/147080664/7de9a05d-bc76-42a7-9874-db87ae163a1a)
![image](https://github.com/RalucaSpt/Dictionary/assets/147080664/38535e9c-6676-4b2f-9b93-351d4beb90fa)

