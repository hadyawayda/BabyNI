import Link from 'next/link'
import Image from 'next/image'

const Sidebar = () => {
   return (
      <div className="min-h-screen flex flex-col justify-center items-start">
         <div className="side-top flex justify-start items-center shrink-0">
            <Image
               src="./yuvo_logo.svg"
               alt="Yuvo Logo"
               width={79.6}
               height={31}
               className="right-border"
            />
            <Image
               src="./network_insight.svg"
               alt="Network Insight Logo"
               width={136}
               height={28}
               className="NI-logo"
            />
         </div>
         <div className="side-bottom flex flex-col justify-start items-center h-full">
            <div className="search-box w-full">
               <input
                  type="text"
                  className="rounded-full search-input text-xs"
                  placeholder="Search..."
               />
               <Link
                  href="#"
                  className="search-logo icon-lock"
               >
                  <Image
                     src="./search-icon.svg"
                     alt="Search Icon"
                     width={18}
                     height={18}
                  />
               </Link>
            </div>
            <div>Remaining elements</div>
         </div>
      </div>
   )
}

export default Sidebar
