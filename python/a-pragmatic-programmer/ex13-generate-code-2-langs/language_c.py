class language_C():
    commentStartSymbol = "/*"
    commentEndSymbol = "*/"

    def simpleType(self, type, fieldName):
        return f"\t{type} {fieldName};"

    def arrayType(self, type, fieldName, size):
        return f"\t{type} {fieldName}[{size}];"
        
    def messageStartLine(self, name):
        return "typedef struct {"

    def messageEndLine(self, name):
        return f"}} {name}Msg;"

    fileExtension = "c"