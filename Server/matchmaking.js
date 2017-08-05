// const _ = require('lodash')
const Game = require('./game')

const stack = []

// We'll add things like ELO equivalent later.
const enqueue = conn => {
  var player = {
    conn: conn,
    ranking: 7
  }
  stack.push(player)
  console.log(`The stack is at ${stack.length} players.`)
}

// The main function fires on an interval, sorting the stack by ranking/whatever, and identifying suitable pairings.
const match = () => {
  if (stack.length >= 2) {
    console.log('Making a game!')
    var newGame = new Game()
    newGame.addConnection(stack.pop().conn)
    newGame.addConnection(stack.pop().conn)
    newGame.send('TURN|1')
  }
  setTimeout(match, 1000)
}

match()

module.exports = {enqueue: enqueue}
