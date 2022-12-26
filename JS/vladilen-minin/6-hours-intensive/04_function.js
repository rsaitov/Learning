// 1 Functions

// Function Declaration
function greet(name) {
    console.log('Hello - ', name)
}

// Function Expression
const greet2 = function greet2(name) {
    console.log('Hello2 - ', name)
}

greet('Rinat')
greet2('Rinat')

console.log(typeof greet)   // function
console.dir(greet)

// 2 Anonymous Function
// let counter = 0
// const interval = setInterval(function () {
//     if (counter == 5) {
//         clearInterval(interval)
//     } else {
//         console.log(++counter)
//     }
// }, 1000)

// 3 Arrow functions

const arrow = (name) => { 
    console.log('Hello - ', name)
}
arrow('arrow')

const arrow2 = name => console.log('Hello - ', name)
arrow2('arrow2')

const pow = num => num ** 2
console.log(pow(5))             // 25


// 4 Default Parameters
const sum = (a = 1, b = 2 * a) => a + b;
console.log(sum(1, 2));        
console.log(sum(40));        
console.log(sum());        

function sumAll(...all) {
    let result = 0;
    for (let num of all) {
        result += num
    }
    return result
}

const rest = sumAll(1, 2, 3, 4, 5)
console.log(rest);


// 5 Closure
function createMember(name) {
    return function (lastName) {
        console.log(name + ' ' + lastName)
    }
}

const logWithLastName = createMember('Rinat')
console.log(logWithLastName);
console.log(logWithLastName('Saitov'));