import { Data } from './Interfaces/Interfaces'
import getData from './_Components/API'
import Body from './_Components/Body'

const App = async () => {
   const data = await getData({ dateRange: 'daily' })

   return (
      <>
         <Body props={data} />
      </>
   )
}

export default App
