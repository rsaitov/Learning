// Strings

const myName = 'Rinat'
const age = 36

function getAge() {
    return age
}

// const output = 'Hello! My name is ' + name + ' and my age is ' + age
const output = `Hello! My name is ${myName} and my age is ${getAge()}.`
console.log(output)

const multiLineOutput = `
    <div>This is div</div>
    <p>This is p</p>
`
console.log(multiLineOutput)

console.log(myName.length);
console.log(myName.toUpperCase());
console.log(myName.toLowerCase());
console.log(myName.charAt(2));
console.log(myName.toLowerCase().startsWith('rin'));
console.log(myName.startsWith('Rin'));
console.log(myName.endsWith('at'));
console.log(myName.repeat(3));
const pwd = '    password    '
console.log(pwd.trim());
console.log(pwd.trimLeft());
console.log(pwd.trimRight());

function logPerson(s, name, age) {
    if (age < 0) {
        age = 'Not borned yet'
    }
    return `${s[0]}${name}${s[1]}${age}${s[2]}`
}

const outputFunc = logPerson`Name: ${myName}, Age: ${age}!`
const outputFunc2 = logPerson`Name: ${myName}, Age: ${-10}!`

console.log(outputFunc);
console.log(outputFunc2);