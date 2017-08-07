// const _ = require('lodash')
const Game = require('./game')

const stack = []

// We'll add things like ELO equivalent later.
const enqueue = conn => {
  conn.army = {}
  stack.push(conn)
  console.log(`The stack is at ${stack.length} players.`)
}

// The main function fires on an interval, sorting the stack by ranking/whatever, and identifying suitable pairings.
const match = () => {
  if (stack.length >= 2) {
    console.log('Making a game!')
    var newGame = new Game()
    var player = stack.shift()
    player.write('FIND|0')
    var enemy = stack.shift()
    enemy.write('FIND|1')
    newGame.addConnection(player)
    newGame.addConnection(enemy)
    // newGame.send('TURN|88')
  }
  setTimeout(match, 1000)
}

match()

module.exports = {enqueue: enqueue}
