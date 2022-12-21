// Number
const num = 42      // integer
console.log(num)

const float = 42.42 // float
console.log(float)

const pow = 10e3    // extent
console.log(pow)

console.log('MAX_SAFE_INTEGER', Number.MAX_SAFE_INTEGER)
console.log('Math.pow', Math.pow(2, 53) - 1)
console.log('MIN_SAFE_INTEGER', Number.MIN_SAFE_INTEGER)
console.log('MAX_VALUE', Number.MAX_VALUE)
console.log('MIN_VALUE', Number.MIN_VALUE)
console.log('POSITIVE_INFINITY', Number.POSITIVE_INFINITY);
console.log('NEGATIVE_INFINITY', Number.NEGATIVE_INFINITY);
console.log('Not A Number', Number.NaN);
console.log('typeof NaN', typeof NaN);

const weird = 2 / undefined
console.log('weird is NaN?', Number.isNaN(weird));
console.log('42 is NaN?', Number.isNaN(42));
console.log(Number.isFinite(Infinity));
console.log(Number.isFinite(42));

const stringInt = '40'
const stringFloat = '40.42'

console.log(stringInt + 2)                      // '402'
console.log(Number.parseInt(stringInt) + 2);    // 42
console.log(parseInt(stringInt) + 2);           // 42
console.log(+stringInt + 2);                    // 42

console.log(parseFloat(stringFloat) + 2);       // 42.42
console.log(+stringFloat + 2);                  // 42.42

console.log(0.4 + 0.2);                 // 0.6000000000000001
console.log((4 / 10) + (2 / 10));       // 0.6000000000000001
console.log(parseFloat((0.4 + 0.2).toFixed(1))); // 0.6

// BigInt
console.log(9007199254740991999n)
console.log(typeof 9007199254740991999n)

//console.log(10n - 4)         // error
console.log(parseInt(10n) - 4)      // 6 int
console.log(10n - BigInt(4))        // 6n BigInt


// 3 Math
console.log(Math.E)
console.log(Math.PI)

console.log(Math.sqrt(25))
console.log(Math.pow(5, 3))
console.log(Math.abs(-42))
console.log(Math.max(42, 12, 23, 11))
console.log(Math.min(42, 12, 23, 11))
console.log(Math.floor(4.5))
console.log(Math.ceil(4.5))
console.log(Math.round(4.5))
console.log(Math.trunc(4.5))
console.log(Math.random())

// 4 Example

function getRandomBetween(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min)
}

console.log(getRandomBetween(10, 42))

console.log()
console.log()