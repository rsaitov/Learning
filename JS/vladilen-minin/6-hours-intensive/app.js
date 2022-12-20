// 1 Variables

var name = 'Rinat'          //obsolete definition
const firstName = 'Rinat'
let age = 36

// console.log(name);

// special symbols in variable name
const _ = 'private'
const $ = 'some value'

// 2 Muting
// console.log('User name: ' + firstName + ', age: ' + age)

//const lastName = prompt('Enter last name')
//alert(firstName + ' ' + lastName)

// 3 Operators
// const currentYear = 2022
// const birthYear = 1986

// const ageFromYears = currentYear - birthYear

// console.log(ageFromYears)

// 4 Data types
const isProgrammer = true
const myName = 'Rinat'
const myAge = 36
let x
// console.log(typeof isProgrammer) // boolean
// console.log(typeof myName) // string
// console.log(typeof myAge) // int
// console.log(typeof x) // indefined
// console.log(typeof null) // return object
// console.log(null) // null

// 5 Operators priority
// const fullAge = 36
// const birthYear = 1986
// const currentYear = 2022

// > < >= <=
// const isFullAge = currentYear - birthYear >= isFullAge

// 6 Сonditional operators
// const courseStatus = 'pending' // ready, fail, pending

// if (courseStatus === 'ready') {
//     console.log('Course is ready')
// } else if (courseStatus === 'pending') {
//     console.log('Course is under construction')
// } else {
//     console.log('Course failed')
// }

// 7 Boolean logic
// https://developer.mozilla.org/ru/docs/Web/JavaScript/Guide/Expressions_and_Operators

// 8 Functions

function calculateAge(year) {
    return 2022 - year
}

// console.log(calculateAge(1986))
// console.log(calculateAge(1990))
// console.log(calculateAge(1994))

function logInfoAbout(name, year) {
    const age = calculateAge(year)

    if (age > 0) {
        console.log('Man with name ' + name + ' is now at age: ' + age)
    } else {
        console.log('Not correct year!')
    }
}

// logInfoAbout('Rinat', 1986)
// logInfoAbout('Alex', 1990)
// logInfoAbout('Dan', 2025)

// 9 Arrays

// const cars = ['BMW', 'Mercedes', 'Ford']
// const cars = new Array('BMW', 'Mercedes', 'Ford');

// console.log(cars)
// console.log(cars[0])
// console.log(cars.length)

// cars[0] = 'Porsche'
// cars[cars.length] = 'Mazda'
// console.log(cars)

// 10 Loops
// const cars = ['BMW', 'Mercedes', 'Ford']

// for loop
// for (let i = 0; i < cars.length; i++) {
//     console.log(i)
//     console.log(cars[i])
// }

// for (let car of cars) {
//     console.log(car)
// }

// 11 Objects
const person = {
    firstName: 'Rinat',
    lastName: 'Saitov',
    year: 1986,
    langages: ['Russian', 'English'],
    hasWife: true,
    greet: function () {
        console.log('greet')
    }
}

console.log(person.firstName)
console.log(person['lastName'])
const key = 'year'
console.log(person[key])
person.isProgrammer = true
console.log(person)