class language_Pascal():
    commentStartSymbol = "{"
    commentEndSymbol = "}"

    def simpleType(self, type, fieldName):
        return f"\t{fieldName}: {type};"

    def arrayType(self, type, fieldName, size):
        return f"\t{fieldName}: array[0-{int(size)-1}] of {type};"
        
    def messageStartLine(self, name):
        return f"{name}Msg = packed record"

    def messageEndLine(self, name):
        return "end;"

    fileExtension = "pas"