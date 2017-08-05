const net = require('net')
const handleMessage = require('./messageHandler')

const server = net.createServer(conn => {
  conn.setEncoding('utf8')
  console.log('client connected')
  conn.on('data', data => {
    handleMessage(conn, data)
  })
  conn.on('error', err => console.log(err))
  conn.on('end', () => console.log('client disconnected'))
})

server.on('error', (err) => {
  console.log(err)
  throw err
})
server.listen(3000, () => {
  console.log('Server listening!')
})
