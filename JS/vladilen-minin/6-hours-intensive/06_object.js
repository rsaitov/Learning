// Objects

const person = {
    name: 'Rinat',
    age: 26,
    isProgrammer: true,
    languages: ['ru', 'en', 'de'],
    'complex key': 'value',
    ['key_' + (1 + 3)]: 'Computed Key', // key_4

    greet: function () {
        console.log('greet from Person')
    },
    greet2() {
        console.log('greet2 from Person')
    },
    info() {
        console.info('Information about person with name: ', this.name)
    }
}

// console.log('person', person);
// console.log(person.name);
// console.log(person['age']);
// console.log(person['complex key']);
// console.log(person.key_4);
// console.log(person['key_4']);

//person.age = undefined
person.languages.push('by')

// Deleting field
delete person['key_4']
// console.log('person', person);

// Destructuring
const { name, age: personAge = 10, languages } = person
// console.log(name, personAge, languages);

// Iterate object keys thru "for .. in"
for (let key in person) {
    if (person.hasOwnProperty(key)) {
        // console.log('key', key)
        // console.log('value', person[key])
    }
}

// Iterate object keys thru "Object.keys"
Object.keys(person)
    .forEach(key => {
        // console.log(key);
        // console.log(person[key]);
    })

// Context
// person.info()

const logger = {
    keys() {
        //console.log('Object Keys: ', Object.keys(this))
    },

    keysAndValues() {
        Object.keys(this).forEach(key => {
            console.log(`"${key}": ${this[key]}`);
        })
    },

    // syntax function changes context
    keysAndValuesWrongContext() {
        // const self = this    workaround
        Object.keys(this).forEach(function (key) {   // this = context function
            console.log(`"${key}": ${this[key]}`);
        })
        //.bind(this)    workaround
    },

    withParams(top = false, between = false, bottom = false) {
        if (top) {
            console.log('-----Start-----')
        }
        Object.keys(this).forEach((key, index, array) => {
            console.log(`"${key}": ${this[key]}`);
            if (between && index !== array.length - 1) {
                console.log('---------------')
            }
        })
        if (bottom) {
            console.log('------End------')
        }
    }
}

// Bind context, call from variable
const bound = logger.keys.bind(person)
bound()

// Bind context, call immediately
console.log(person)
logger.keys.call(person)
logger.keysAndValues.call(person)

// Method params
logger.withParams.call(person, true, true, true)

// Method params as array
logger.withParams.apply(person, [true, true, true])