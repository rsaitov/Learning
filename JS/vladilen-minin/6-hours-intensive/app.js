// DOM

// window.alert('1')
// window.prompt('What is your name?')
// window.confirm('Confirm statement')

const heading = document.getElementById('hello')

// obsolete
// const heading2 = document.getElementsByTagName('h2')[0]

// obsolete
// const heading2 = document.getElementsByClassName('h2-class')[0]

// const heading2 = document.querySelector('.h2-class')
const heading2 = document.querySelector('#sub-hello')  // only 1 element!
console.dir(heading2);

const heading3 = heading2.nextElementSibling
console.dir(heading3);

const h2list = document.querySelectorAll('h2')
console.dir(h2list);

setTimeout(() => {
    addStylesTo(heading, 'JavaScript')
}, 1500)

const link = heading3.querySelector('a')
link.addEventListener('click', () => {
    event.preventDefault()
    console.log('Click on link!', );

    const url = event.target.getAttribute('href')
    window.location = url
})

setTimeout(() => {
    addStylesTo(link, 'Practice', 'blue')
}, 500)

setTimeout(() => {
    addStylesTo(heading2, 'All be good!', 'yellow', '2rem')
}, 4500)

function addStylesTo(node, text, color='red', fontSize) {
    node.textContent = text
    node.style.color = color
    node.style.textAlign = 'center'
    node.style.backgroundColor = 'black'
    node.style.padding = '2rem'
    node.style.display = 'block'
    node.style.width = '100%'

    if (fontSize) {
        node.style.fontSize = fontSize
    }
}

// https://developer.mozilla.org/ru/docs/Web/Events

heading.onclick = () => {
    if (heading.style.color === 'red') {
        heading.style.color = '#000'
        heading.style.backgroundColor = '#fff'
    } else {        
        heading.style.color = 'red'
        heading.style.backgroundColor = '#000'
    }
}

heading2.addEventListener('dblclick', () => {
    if (heading2.style.color === 'yellow') {
        heading2.style.color = '#000'
        heading2.style.backgroundColor = '#fff'
    } else {        
        heading2.style.color = 'yellow'
        heading2.style.backgroundColor = '#000'
    }
})