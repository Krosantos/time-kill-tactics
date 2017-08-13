const net = require('net')
const sendMessage = require('./utilities/sendMessage')
const handler = require('./handler')

const server = net.createServer(conn => {
  conn.setEncoding('utf8')
  console.log('client connected')
  conn.on('data', data => {
    handler.message(conn, data)
  })
  conn.on('error', err => handler.error(conn, err))
  conn.on('end', () => handler.disconnect(conn))
  conn.send = (type, body, delay) => {
    setTimeout(() => sendMessage(type, body, conn), delay || 100)
  }
})

server.on('error', (err) => {
  console.log(err)
  throw err
})

server.listen(3000, () => {
  console.log('Server listening!')
})
