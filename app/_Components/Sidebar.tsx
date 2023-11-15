import Link from 'next/link'

const Sidebar = () => {
   return (
      <div className="min-h-screen flex flex-col justify-center items-start">
         <div className="side-top flex justify-center items-center shrink-0">
            Top
         </div>
         <div className="side-bottom flex flex-col justify-start items-center h-full">
            <div className="search-box w-full flex justify-end">
               <input
                  type="text"
                  className="rounded-full search-input text-xs placeholder:text-zinc-300"
                  placeholder="Search..."
               />
               <Link
                  href="#"
                  className="search-logo icon-lock"
               ></Link>
            </div>
            <div>Remaining elements</div>
         </div>
      </div>
   )
}

export default Sidebar
