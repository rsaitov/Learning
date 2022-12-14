from os import listdir
import os
from os.path import isfile, join
import shutil

fileFolder = "Files"
filesInFolder = [f for f in listdir(fileFolder) 
    if isfile(join(fileFolder, f)) and f.endswith("pl")]

for fileName in filesInFolder:
    filePath = os.path.join(fileFolder, fileName)
    with open(filePath) as file:
        fileLines = [line.rstrip() for line in file]

    useStrictExists = False
    commentBlockStarted = False
    commentBlockFinished = False
    firstLineIndexAfterCommentBlock = 0

    for index, line in enumerate(fileLines):
        if (not commentBlockStarted and line.startswith('#')):
            commentBlockStarted = True

        if (commentBlockStarted and not commentBlockFinished and not line.startswith('#')):
            commentBlockFinished = True
            firstLineIndexAfterCommentBlock = index

        if (commentBlockFinished and line.startswith("use strict")):
            useStrictExists = True

    print(f"'use strict' directive exists flag: {useStrictExists}. File: {fileName}")

    if not useStrictExists:
        fileLines.insert(firstLineIndexAfterCommentBlock, "use strict;")
        print(f"Inserted 'use strict;' into line {firstLineIndexAfterCommentBlock}")

        filePathCopy = os.path.join(fileFolder, f"${fileName}.bak");
        shutil.copyfile(filePath, filePathCopy)
        
        with open(filePath, "w") as file:
            for line in fileLines:
                file.write(line + '\n')
