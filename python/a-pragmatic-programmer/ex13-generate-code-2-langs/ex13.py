import re
from language_pascal import language_Pascal
from language_c import language_C

sourceFileName = "source.txt"
commentRegexString = r"^\#\s*(.+)$"
messageRegexString = r"^M\s*(.*)$"
simpleTypeRegexString = r"^F\s*(\w+)\s+(\w+)$"
arrayTypeRegexString = r"^F\s*(\w+)\s+(\w+)\[(\d+)\]$"

languageSyntax = language_C()
# languageSyntax = language_Pascal()

resultFileName = f"result.{languageSyntax.fileExtension}"

with open(sourceFileName) as file:
    fileLines = [line.rstrip() for line in file]

newFileLines = []
messageName = "unknown"

for line in fileLines:

    commentMatch = re.match(commentRegexString, line)
    if (commentMatch):
        newFileLines.append(f"{languageSyntax.commentStartSymbol} {commentMatch.group(1)} {languageSyntax.commentEndSymbol}")
    
    messageMatch = re.match(messageRegexString, line)
    if(messageMatch):
        messageName = messageMatch.group(1)
        newFileLines.append(languageSyntax.messageStartLine(messageName))

    simpleTypeMatch = re.match(simpleTypeRegexString, line)
    if (simpleTypeMatch):
        fieldName = simpleTypeMatch.group(1)
        type = simpleTypeMatch.group(2)
        newFileLines.append(languageSyntax.simpleType(type, fieldName)) 

    arrayTypeMatch = re.match(arrayTypeRegexString, line)
    if (arrayTypeMatch):
        fieldName = arrayTypeMatch.group(1)
        type = arrayTypeMatch.group(2)
        arrayLength = arrayTypeMatch.group(3)
        newFileLines.append(languageSyntax.arrayType(type, fieldName, arrayLength)) 

    if(line.startswith("E")):
        newFileLines.append(languageSyntax.messageEndLine(messageName))

with open(resultFileName, "w") as file:
    for line in newFileLines:
        file.write(line + '\n')