// Asynchronous
// Event Loop

// const timeout = setTimeout(() => {
//     console.log('After timeout');
// }, 1500)

// clearTimeout(timeout)

// const interval = setInterval(() => {
//     console.log('After interval');
// }, 1500)

// clearInterval(timeout)

// const delay = (callback, wait = 1000) => {
//     setTimeout(callback, wait)
// }

// delay(() => {
//     console.log('After 2 seconds');
// }, 2000)

// Promises
const delay = (wait = 1000) => {
    const promise = new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve()
            // reject('Something went wrong')
        }, wait)
    })    
    return promise
}

// delay(2500)
//     .then(() => console.log('After 2 seconds'))
//     .catch((err) => console.error('Error', err))
//     .finally(() => console.log('Finally'))

const getData = () => new Promise(resolve => resolve(
    [1, 1, 2, 3, 5, 8]
))

// Using then statement
// getData().then(data => console.log(data))

// Using async-await keywords
async function asyncExample() {
    await delay(2000)
    const data = await getData()
    console.log(data)
}

asyncExample()