// Array

const cars = ['Mazda', 'Ford', 'BMW', 'Mercedes']
const fib = [1, 1, 2, 3, 5, 8, 13]
const various = [1, 1, 2, 3, 5, 8, 13, '21', true, {}]

// // Function
// function addItemToEnd() {

// }

// // Method
// cars.push('Renault')
// cars.unshift('Peugeot')
// console.log(cars);

// const firstItem = cars.shift()
// const lastItem = cars.pop()
// console.log(cars);

// console.log(cars.reverse());
// console.log(cars);

// indexOf() - simple search
const indexBMW = cars.indexOf('BMW')
console.log('indexBMW=', indexBMW);     // indexBMW= 2
cars[indexBMW] = 'Lada'
console.log(cars);

// findIndex(), find() - complex find function
const people = [
    { name: 'Rinat', budget: 4200 },
    { name: 'Ivan', budget: 1200 },
    { name: 'Petr', budget: 2400 }
]
const personIndex = people.findIndex((person) => person.budget == 1200)
const person = people.find((person) => person.budget == 1200)

console.log(personIndex)
console.log(person)

// includes()
console.log(cars.includes('BMW'))

// map()
const upperCaseCars = cars.map(car => car.toUpperCase())
console.log(upperCaseCars);
console.log(cars);

const pow2 = num => num ** 2
const pow2Fib = fib.map(pow2)
const powAndBack = fib.map(pow2).map(Math.sqrt)
console.log(pow2Fib);

// filter()
const filteredNumbers = pow2Fib.filter(num => {
    return num > 20
})
console.log('filteredNumbers', filteredNumbers);
console.log('pow2Fib', pow2Fib);

// reduce() 
const allBudget = people
    .filter(person => person.budget > 2000) // chaining here
    .reduce((acc, person) => {
        acc += person.budget
        return acc
    }, 0)

console.log('allBudget', allBudget);

// Task 1
// const text = 'Hello, we learn JavaScript'
// const reverseText = text.split('').reverse().join('')
// console.log(reverseText);