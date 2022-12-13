# Your C program uses an enumerated type to represent one of 100 states.
# on p. 285 You’d like to be able to print out the state as a string (as opposed to a
# number) for debugging purposes. Write a script that reads from standard
# input a file containing
#
# name
# state_a
# state_b

from os.path import exists

def handleFile(stateFileName):
    with open(stateFileName) as file:
        fileLines = [line.rstrip() for line in file]

    # read type name
    name = fileLines[0]

    hFileName = f"{name}.h"
    cFileName = f"{name}.c"

    hFileContent = f"""extern const char* {name.upper()}_names[];
typedef enum {{
"""
    cFileContent = f"""const char* {name.upper()}_names [] = {{
""";

    for index,state in enumerate(fileLines[1:]):
        if (index != 0):
            hFileContent += ",\n"
            cFileContent += ",\n"

        hFileContent += f"\t{state}"
        cFileContent += f"\t\"{state}\""

    hFileContent += f"\n}} {name.upper()};"
    cFileContent += f"\n}};"

    print(hFileName)
    print(hFileContent)
    print(cFileName)
    print(cFileContent)

    hTextFile = open(hFileName, "w")
    n = hTextFile.write(hFileContent)
    hTextFile.close()

    cTextFile = open(cFileName, "w")
    n = cTextFile.write(cFileContent)
    cTextFile.close()

stateFileName = "states.txt"
fileExists = exists(stateFileName)

if (fileExists == False):
    print(f"No file found {stateFileName}")
else:
    handleFile(stateFileName)