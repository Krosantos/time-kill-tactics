const _ = require('lodash')
const net = require('net')
const handler = require('./handler')

const server = net.createServer(conn => {
  conn.setEncoding('utf8')
  console.log('client connected')
  conn.on('data', data => {
    handler.message(conn, data)
  })
  conn.on('error', err => handler.error(conn, err))
  conn.on('end', () => handler.disconnect(conn))
  conn.send = (msg, delay) => {
    setTimeout(() => conn.write(msg), delay || 100)
  }
})

server.on('error', (err) => {
  console.log(err)
  throw err
})

server.listen(3000, () => {
  console.log('Server listening!')
})
