const net = require('net')

const server = net.createServer(conn => {
  conn.setEncoding('utf8')
  console.log('client connected')
  conn.on('end', () => console.log('client disconnected'))
  conn.on('data', data => {
    console.log(data)
    conn.write('Wut u doin?')
  })
  conn.on('error', err => console.log(err))
})

server.on('error', (err) => {
  console.log(err)
  throw err
})
server.listen(3000, () => {
  console.log('Server listening!')
})
